using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    public RopeHandler myRope;

    public bool beingHeld = false;
    public bool beenPickedUp = false;

    private Rigidbody myRigidBody;

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
        if ( collision.transform.tag == "Hookable" && beenPickedUp && !beingHeld)
        {
            transform.SetParent(collision.transform);
            myRigidBody.isKinematic = true;
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
        myRope.simpleLineToWinch();
        if (beenPickedUp && !beingHeld) transform.LookAt(transform.position + myRigidBody.velocity*10f);
        if (transform.position.y < -100) Destroy(gameObject);
    }
}
