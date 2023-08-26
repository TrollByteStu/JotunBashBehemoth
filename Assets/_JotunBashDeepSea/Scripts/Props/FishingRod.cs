using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public GameObject BobberPrefab;
    public Rigidbody AttachmentRigidBody;


    public bool beingHeld = false;
    public bool beenPickedUp = false;

    private fishingBobber myBobber;
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
        myBobber.eventActivate();
    }

    public void eventDeactiveate()
    {
        myBobber.eventDeactiveate();
    }

    void SpawnFishingBobber()
    {
        var bobber = Instantiate(BobberPrefab, AttachmentRigidBody.transform.position, Quaternion.identity);
        myBobber = bobber.GetComponent<fishingBobber>();
        bobber.GetComponent<SpringJoint>().connectedBody = AttachmentRigidBody;
        bobber.GetComponentInChildren<StringHandler>().stringAttach = AttachmentRigidBody;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        SpawnFishingBobber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
