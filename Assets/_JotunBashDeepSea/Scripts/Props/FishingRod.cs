using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{

    public bool beingHeld = false;
    public bool beenPickedUp = false;

    private Rigidbody myRigidBody;

    public void eventSelect()
    {
        Debug.Log("Select");
        beingHeld = true;
        beenPickedUp = true;
    }

    public void eventUnSelect()
    {
        myRigidBody.isKinematic = false;
        transform.SetParent(null);
        Destroy(gameObject, 10f);
    }

    public void eventActivate()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
