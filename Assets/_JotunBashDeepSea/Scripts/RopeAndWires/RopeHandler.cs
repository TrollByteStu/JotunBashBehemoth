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
    private Transform TheWinch;

    public void simpleLineToWinch()
    {
        myLine.SetPosition(0, transform.position);
        myLine.SetPosition(1, TheWinch.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        myLine = GetComponent<LineRenderer>();
        TheWinch = GameObject.Find("RaftWinch").transform;
        myLine.positionCount = 2;
        myLine.SetWidth(.005f, .005f);
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
            for (int i = 0; i < transform.childCount; i++)
            {
                myLine.SetPosition(i, transform.GetChild(i).position);
            }
        }
    }

    public void spawnFromPointToPoint()
    {
        int count = (int)(Vector3.Distance(transform.position, TheWinch.position) / segmentDistance);

        GameObject tmp;
        GameObject lastTmp = gameObject;
        Vector3 tmpPos;
        float steps = 1 / count;

        for (int i = 0; i < count; i++)
        {
            tmpPos = Vector3.Lerp( TheWinch.position, transform.position, steps * i);
            tmp = Instantiate(SegmentPrefab, tmpPos, Quaternion.identity, transform);
            tmp.name = "Rope Segment " + i.ToString();

            if (i == 0)
            {
                //Destroy(tmp.GetComponent<CharacterJoint>());
                //tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                tmp.GetComponent<CharacterJoint>().connectedBody = TheWinch.gameObject.GetComponent<Rigidbody>();
            }
            else
            {
                tmp.GetComponent<CharacterJoint>().connectedBody = lastTmp.GetComponent<Rigidbody>();
            }
            lastTmp = tmp;
        }
        lastTmp.transform.position = transform.position;
        lastTmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        lastTmp.GetComponent<Rigidbody>().isKinematic = true;
    }

    void simpleSpawn()
    {
        return;
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
