using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GannetEnterWater : MonoBehaviour
{
    float _Delay;
    private void OnTriggerEnter(Collider other)
    {
        if (_Delay + 1 < Time.unscaledTime)
        {
            GetComponentInParent<Gannet>().OnWaterEnter(this);
            _Delay = Time.unscaledTime;
        }
    }
}
