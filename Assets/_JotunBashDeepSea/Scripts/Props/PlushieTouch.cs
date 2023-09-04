using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlushieTouch : MonoBehaviour
{
    public Plushie myPlushie;
    public int touchNumber;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") myPlushie.PlushieTouchReportTouch(touchNumber);
    }
}
