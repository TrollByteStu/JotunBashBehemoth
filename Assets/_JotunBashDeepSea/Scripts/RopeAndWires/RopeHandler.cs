using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHandler : MonoBehaviour
{
    [SerializeField]
    GameObject SegmentPrefab;

    [SerializeField]
    float segmentDistance = 0.21f;

    private LineRenderer myLine;
    public Winch TheWinch;

    public void simpleLineToWinch()
    {
        myLine.SetPosition(0, transform.position);
        myLine.SetPosition(1, TheWinch.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        myLine = GetComponent<LineRenderer>();
        TheWinch = GameObject.Find("RaftWinch").GetComponent<Winch>();
        myLine.positionCount = 2;
        myLine.startWidth = 0.005f;
        myLine.endWidth = 0.005f;
    }

    // Update is called once per frame
    void Update()
    {
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
        int count = (int)(Vector3.Distance(transform.position, TheWinch.transform.position) / segmentDistance);

        GameObject tmp;
        GameObject lastTmp = gameObject;
        Vector3 tmpPos;
        float steps = 1 / count;

        for (int i = 0; i < count; i++)
        {
            tmpPos = Vector3.Lerp( TheWinch.transform.position, transform.position, steps * i);
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

}
