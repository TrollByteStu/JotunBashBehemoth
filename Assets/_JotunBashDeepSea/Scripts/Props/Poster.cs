using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poster : MonoBehaviour
{
    private bool beingHeld = false;
    private bool beenPickedUp = false;

    private Rigidbody myRigidBody;

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
        Destroy(gameObject, 10);
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100f) Destroy(gameObject);
    }
}
