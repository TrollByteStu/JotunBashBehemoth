using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class lifeJacket : MonoBehaviour
{

    private Bait myBait;
    private Rigidbody myRigidBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Water")
        {
            GetComponent<OurWateverVolumeFloater>().enabled = true;
            myRigidBody.drag = 1f;
            myRigidBody.angularDrag = 1f;
            myRigidBody.isKinematic = false;
            myBait.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myBait = GetComponent<Bait>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
