using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoatTilt : MonoBehaviour
{
    public Transform tilfForward;
    public Transform tilfBackward;
    public Transform tilfLeft;
    public Transform tilfRight;

    public float tiltScale = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the height difference.
        float leftRightDifference = tilfRight.position.y - tilfLeft.position.y;

        // Calculate the height difference.
        float ForwardBackwardDifference = tilfForward.position.y - tilfBackward.position.y;

        // Apply the rotation to the raft.
        transform.rotation = Quaternion.Euler(ForwardBackwardDifference * tiltScale, 0, leftRightDifference * tiltScale);
    }
}
