using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Hook : MonoBehaviour
{

    public RopeHandler myRope;

    public bool beingHeld = false;
    public bool beenPickedUp = false;
    public bool hooked = false;

    private Rigidbody myRigidBody;
    private Rigidbody hookedRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Water")
        {
            GameObject spawn;
            Quaternion spawnDirection;
            spawn = GameObject.Find("GameController").GetComponent<GameController>().gcResources.Splashes[0];
            spawnDirection = Quaternion.identity;
            GameObject decal = Instantiate(spawn, transform.position, spawnDirection);
            Destroy(decal, 4f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.transform.tag == "Hookable" && beenPickedUp && !beingHeld && !hooked)
        {
            transform.SetParent(collision.transform);
            myRigidBody.isKinematic = true;
            hooked = true;
            myRope.spawnFromPointToPoint();
            hookedRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Destroy( GetComponent<XRGrabInteractable>() );
        }
    }

    public void eventSelect()
    {
        Debug.Log("Select");
        beingHeld = true;
        beenPickedUp = true;
    }

    public void eventUnSelect()
    {
        Debug.Log("UnSelect");
        myRigidBody.isKinematic = false;
        beingHeld = false;
        transform.SetParent(null);
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!hooked) 
        { 
            myRope.simpleLineToWinch();
        } else {
            hookedRigidbody.AddForce(myRope.TheWinch.transform.position - hookedRigidbody.transform.position );
            myRope.TheWinch.PlaySound = 0.5f;
        }
        if (beenPickedUp && !beingHeld) transform.LookAt(transform.position + myRigidBody.velocity*10f);
        if (transform.position.y < -100) Destroy(gameObject);
    }
}
