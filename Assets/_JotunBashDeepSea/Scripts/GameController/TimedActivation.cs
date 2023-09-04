using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedActivation : MonoBehaviour
{
    public float ActivateOnTimeLeft = 300f;

    // Update is called once per frame
    void Update()
    {
        if ( !transform.GetChild(0).gameObject.activeSelf )
        {
            if (GameController.Instance.secondsLeft <= ActivateOnTimeLeft) 
                transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
