using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldNpcHandler : MonoBehaviour
{

    public void InworldNpcFinishedGoal(string goal)
    {
        Debug.Log("goal complete: "+goal);
        if (goal == "shutup")
        {
            Debug.Log("told to shut up");
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
