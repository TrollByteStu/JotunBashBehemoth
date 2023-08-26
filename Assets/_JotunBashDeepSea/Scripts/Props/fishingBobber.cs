using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class fishingBobber : MonoBehaviour
{
    public enum states { hanging,flying, bobbing, reeling }
    public states currentState = states.hanging;

    private SpringJoint mySpringJoint;
    private WateverVolumeFloater myFloater;
    private GameController mainGC;
    private StringHandler myStringHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Water" && currentState == states.flying)
        {
            GameObject spawn;
            Quaternion spawnDirection;
            spawn = mainGC.gcResources.Splashes[0];
            spawnDirection = Quaternion.identity;
            GameObject decal = Instantiate(spawn, transform.position, spawnDirection);
            Destroy(decal, 4f);
            myFloater.enabled = true;
            mySpringJoint.connectedBody = myStringHandler.stringAttach;
            mySpringJoint.maxDistance = Vector3.Distance(transform.position, myStringHandler.stringAttach.transform.position);
        }
    }

    public void eventActivate()
    {
        if ( currentState == states.hanging )
        {
            currentState = states.flying;
            mySpringJoint.connectedBody = null;
        }
    }

    public void eventDeactiveate()
    {

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
        
    }
}
