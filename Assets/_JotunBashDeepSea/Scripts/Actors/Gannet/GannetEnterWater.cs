using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GannetEnterWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<Gannet>().OnWaterEnter(this);
    }
}
