using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public bool _BeingHeld = false;
    public bool _Touched = false;
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

    public void eventUnSelect()
    {
        _BeingHeld = false;
        _Rigidbody.isKinematic = false;
        transform.SetParent(null);

    }

    public void eventActivate()
    {
        // time scale stuff
    }
}
