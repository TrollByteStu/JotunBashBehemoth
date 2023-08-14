using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoMain : MonoBehaviour
{
    private GameObject BoatRig;
    private Vector3 hoverLocation;
    private Vector3 orbit;
    private float StageTimeLeft = 15f;

    public enum Stages { MoveIn , Action , MoveOut };
    public Stages currentStage = Stages.MoveIn;

    private void stageMoveIn()
    {
        transform.position = Vector3.Lerp(transform.position, hoverLocation + orbit, Time.deltaTime * 0.2f);
        if (Vector3.Distance(transform.position, hoverLocation+ orbit) < 5f)
        {
            currentStage = Stages.Action;
        }
    }
    private void stageAction()
    {
        StageTimeLeft -= Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, hoverLocation + orbit, Time.deltaTime * 0.2f);
        if (StageTimeLeft <= 0f)
        {
            hoverLocation = new Vector3(500f, 0, 0);
            currentStage = Stages.MoveOut;
        }
    }
    private void stageMoveOut()
    {
        transform.position = Vector3.Lerp(transform.position, hoverLocation , Time.deltaTime * 0.2f);
        if (Vector3.Distance(transform.position,hoverLocation) < 100f)
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
            hoverLocation = BoatRig.transform.position + (Vector3.up * 20f);
        } else { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        orbit = new Vector3(Mathf.Cos(Time.unscaledTime * .1f) * 40f, 0, Mathf.Sin(Time.unscaledTime * .1f) * 40f);
        switch (currentStage)
        {
            case Stages.MoveIn: stageMoveIn(); break;
            case Stages.Action: stageAction(); break;
            case Stages.MoveOut: stageMoveOut(); break;
        }
        
    }
}
