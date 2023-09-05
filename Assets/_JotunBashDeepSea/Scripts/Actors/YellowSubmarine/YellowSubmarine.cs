using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSubmarine : MonoBehaviour
{
    private GameObject BoatRig;
    private Vector3 hoverLocation;
    private Vector3 orbit;
    public float StageTimeLeft = 15f;

    public enum Stages { MoveIn, Action, MoveOut };
    public Stages currentStage = Stages.MoveIn;

    private AudioSource myAS;

    private void stageMoveIn()
    {
        transform.position = Vector3.Lerp(transform.position, hoverLocation + orbit, Time.deltaTime * 0.3f);
        if (Vector3.Distance(transform.position, hoverLocation + orbit) < 10f)
        {
            currentStage = Stages.Action;
        }
    }
    private void stageAction()
    {
        StageTimeLeft -= Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, hoverLocation + orbit, Time.deltaTime * 0.3f);
        if (StageTimeLeft <= 0f)
        {
            hoverLocation = new Vector3(0, -50, 0);
            currentStage = Stages.MoveOut;
        }
    }
    private void stageMoveOut()
    {
        transform.position = Vector3.Lerp(transform.position, hoverLocation + orbit, Time.deltaTime * 0.1f);
        if (Vector3.Distance(transform.position, hoverLocation + orbit) < 20f)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BoatRig = GameObject.Find("BoatRig");
        if (BoatRig != null)
        {
            hoverLocation = BoatRig.transform.position;
        }
        else { Destroy(gameObject); }
        myAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        orbit = new Vector3(Mathf.Cos(Time.unscaledTime * .05f) * -35f, 0, Mathf.Sin(Time.unscaledTime * .05f) * -70f);
        transform.LookAt(orbit+ new Vector3(0,transform.position.y,0), Vector3.up);
        myAS.volume = 1 + (transform.position.y*0.1f);
        switch (currentStage)
        {
            case Stages.MoveIn: stageMoveIn(); break;
            case Stages.Action: stageAction(); break;
            case Stages.MoveOut: stageMoveOut(); break;
        }
    }
}
