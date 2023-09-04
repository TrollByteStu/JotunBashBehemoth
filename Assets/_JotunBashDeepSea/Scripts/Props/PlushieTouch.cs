using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlushieTouch : MonoBehaviour
{
    public Plushie myPlushie;
    public int touchNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") myPlushie.PlushieTouchReportTouch(touchNumber);
    }

}
