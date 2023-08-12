using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHandler : MonoBehaviour
{
    [SerializeField]
    GameObject SegmentPrefab;

    [SerializeField]
    [Range(1, 1000)]
    int LengthOfRope = 5;

    [SerializeField]
    float segmentDistance = 0.21f;

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;


    private LineRenderer myLine;

    // Start is called before the first frame update
    void Start()
    {
        myLine = GetComponent<LineRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            foreach(GameObject tmp in transform)
            {
                    Destroy(tmp);
                    reset = false;
            }
        }
        
        if (spawn)
        {
            simpleSpawn();
            spawn = false;
        }

        if ( transform.childCount > 0)
        {
            myLine.positionCount = transform.childCount;
            myLine.SetWidth(.005f, .005f);
            for (int i = 0; i < transform.childCount; i++)
            {
                myLine.SetPosition(i, transform.GetChild(i).position);
            }
        }
    }

    void simpleSpawn()
    {
        int count = (int)(LengthOfRope / segmentDistance);

        GameObject tmp;
        GameObject lastTmp = gameObject;

        for (int i = 0; i < count; i++)
        {
            tmp = Instantiate(SegmentPrefab, new Vector3(transform.position.x, transform.position.y+(segmentDistance*(i+1)), transform.position.z), Quaternion.identity, transform);
            tmp.name = "Rope Segment " + i.ToString();

            if (i == 0)
            {
                Destroy(tmp.GetComponent<CharacterJoint>());
                if (snapFirst) tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            } else
            {
                tmp.GetComponent<CharacterJoint>().connectedBody = lastTmp.GetComponent<Rigidbody>();    
            }
            lastTmp = tmp;
        }

        if (snapLast) lastTmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
