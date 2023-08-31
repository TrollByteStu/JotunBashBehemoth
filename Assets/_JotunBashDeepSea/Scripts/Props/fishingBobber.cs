using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class fishingBobber : MonoBehaviour
{
    public FishingRod myFishingRod;
    public Transform myCatchHolder;
    public GameObject myCatchPrefab;

    public enum states { hanging,flying, bobbing, reeling }
    public states currentState = states.hanging;

    private bool floating = false;
    private float floatUntilBite = 10f;
    private bool gotHooked = false;

    private SpringJoint mySpringJoint;
    private OurWateverVolumeFloater myFloater;
    private StringHandler myStringHandler;
    private Rigidbody myRigidBody;

    private void addSpringJoint()
    {
        mySpringJoint = gameObject.AddComponent<SpringJoint>();
        mySpringJoint.connectedBody = myStringHandler.stringAttach;
        mySpringJoint.maxDistance = Vector3.Distance(transform.position, myStringHandler.stringAttach.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Water" && (currentState == states.flying || currentState == states.reeling))
        {
            GameObject spawn;
            Quaternion spawnDirection;
            spawn = GameController.Instance.gcResources.Splashes[0];
            spawnDirection = Quaternion.identity;
            GameObject decal = Instantiate(spawn, transform.position, spawnDirection);
            Destroy(decal, 4f);
            myFloater.enabled = true;
            floating = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Raft")
        { // this has been picked up, add to inventory
            //mainGC.gcInventory.itemAdd(AddInventoryPrefab, AddInventoryAmount);
            Destroy(gameObject);
        }
    }

    public void eventActivate()
    {
        if ( currentState == states.hanging )
        {
            currentState = states.flying;
            Destroy(mySpringJoint);
            return;
        }
        if ( currentState == states.flying )
        {
            currentState = states.reeling;
            mySpringJoint = gameObject.AddComponent<SpringJoint>();
            mySpringJoint.connectedBody = myStringHandler.stringAttach;
            mySpringJoint.maxDistance = Vector3.Distance(transform.position, myStringHandler.stringAttach.transform.position);
            return;
        }
        if ( currentState == states.bobbing )
        {
            currentState = states.reeling;
        }
    }

    public void eventDeactiveate()
    {
        if ( currentState == states.reeling)
        {
            if ( floating)
            {
                currentState = states.bobbing;
            } else {
                currentState = states.flying;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mySpringJoint = GetComponent<SpringJoint>();
        myFloater = GetComponent<OurWateverVolumeFloater>();
        myStringHandler = GetComponentInChildren<StringHandler>();
        myRigidBody = GetComponent<Rigidbody>();
        floatUntilBite = Random.Range(3f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!myFishingRod) Destroy(gameObject);
        if (floating)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            floatUntilBite -= Time.deltaTime;
            if ( floatUntilBite < 0f && !gotHooked)
            {
                gotHooked = true;
                Instantiate(myCatchPrefab, myCatchHolder.position, myCatchHolder.rotation, myCatchHolder);
                myFloater.offset = -1f;
            }
            if ( gotHooked )
            {
                myFloater.offset = Mathf.Lerp(myFloater.offset, 0f, Time.deltaTime);
                if (Random.Range(0, 200) == 1) myFloater.offset = -1f;
            }
        }
        if ( currentState == states.reeling)
        {
            myRigidBody.AddForce((myFishingRod.AttachmentRigidBody.position - transform.position)*Time.deltaTime);
        }
    }
}
