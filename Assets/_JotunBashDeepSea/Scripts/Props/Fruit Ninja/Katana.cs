using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public bool _BeingHeld = false;
    public bool _Touched = false;

    float _CurrentTimeScale;
    public float _MinTimeScale;
    public bool _SlowTime;
    private Rigidbody _Rigidbody;

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }


    public void eventSelect()
    {
        _BeingHeld = true;
        _Touched = true;
    }

    public void eventDeselect()
    {
        _BeingHeld = false;
        _Rigidbody.isKinematic = false;
        transform.SetParent(null);

    }

    public void eventActivate()
    {
        // time scale stuff
        Time.timeScale = _MinTimeScale;
    }

    public void eventDeactivate()
    {
        Time.timeScale = 1;
    }
}
