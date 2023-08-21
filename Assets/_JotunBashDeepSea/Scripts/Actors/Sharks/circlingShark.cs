using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class circlingShark : MonoBehaviour
{
    private GameObject BoatRig;
    private Vector3 orbit;
    private bool reachedSurface = false;

    public GameObject myOwnPrefab;

    public float distance = 20f;
    public float speed = .1f;

    private Rigidbody myRigidBody;
    private GameController mainGC;

    // Start is called before the first frame update
    void Start()
    {
        BoatRig = GameObject.Find("BoatRig");
        myRigidBody = GetComponent<Rigidbody>();
        mainGC = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        orbit = new Vector3(Mathf.Cos(Time.unscaledTime * speed) * distance, 0, Mathf.Sin(Time.unscaledTime * speed) * distance);
        transform.position = Vector3.Lerp(transform.position, BoatRig.transform.position + orbit, Time.deltaTime * 0.2f);
        transform.LookAt(BoatRig.transform);
        transform.Rotate(Vector3.up, 90);
        if ( !reachedSurface )
        {
            if (transform.position.y > -0.5f)
            {
                GetComponent<WateverVolumeFloater>().enabled = true;
                reachedSurface = true;
                myRigidBody.useGravity = true;
            }

        } else {

        }
    }
}
