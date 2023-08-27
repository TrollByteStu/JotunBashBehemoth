using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class fishingBobber : MonoBehaviour
{
    public FishingRod myFishingRod;

    public enum states { hanging,flying, bobbing, reeling }
    public states currentState = states.hanging;

    private bool hitTheWater = false;

    private SpringJoint mySpringJoint;
    private WateverVolumeFloater myFloater;
    private GameController mainGC;
    private StringHandler myStringHandler;

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
            spawn = mainGC.gcResources.Splashes[0];
            spawnDirection = Quaternion.identity;
            GameObject decal = Instantiate(spawn, transform.position, spawnDirection);
            Destroy(decal, 4f);
            myFloater.enabled = true;
            hitTheWater = true;
            addSpringJoint();
        }
    }

    public void eventActivate()
    {
        if ( currentState == states.hanging )
        {
            currentState = states.flying;
            mySpringJoint.connectedBody = null;
        }
        if ( currentState == states.flying )
        {
            currentState = states.reeling;
            mySpringJoint = gameObject.AddComponent<SpringJoint>();
            mySpringJoint.connectedBody = myStringHandler.stringAttach;
            mySpringJoint.maxDistance = Vector3.Distance(transform.position, myStringHandler.stringAttach.transform.position);
        }
        if ( currentState == states.bobbing )
        {
            currentState = states.reeling;
            mySpringJoint.connectedBody = myStringHandler.stringAttach;
            mySpringJoint.maxDistance = Vector3.Distance(transform.position, myStringHandler.stringAttach.transform.position);
        }
    }

    public void eventDeactiveate()
    {
        if ( currentState == states.reeling)
        {
            if ( hitTheWater)
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
        myFloater = GetComponent<WateverVolumeFloater>();
        mainGC = GameObject.Find("GameController").GetComponent<GameController>();
        myStringHandler = GetComponentInChildren<StringHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myFishingRod) Destroy(gameObject);
        if ( currentState == states.reeling)
        {
            mySpringJoint.maxDistance -= Time.deltaTime * 0.5f;
            if ( Vector3.Distance(transform.position, myStringHandler.stringAttach.transform.position) < 3f && hitTheWater )
            {
                myFloater.enabled = false;
                hitTheWater = false;
            }
            if ( Vector3.Distance(transform.position, myStringHandler.stringAttach.transform.position) < 1f && !hitTheWater )
            {
                mySpringJoint.maxDistance = .5f;
                currentState = states.hanging;
            }
        }
    }
}
