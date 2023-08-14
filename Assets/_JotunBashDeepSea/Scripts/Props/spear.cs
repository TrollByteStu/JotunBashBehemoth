using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spear : MonoBehaviour
{
    public bool beingHeld = false;
    private Rigidbody myRB;

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }
    public void eventSelect()
    {
        beingHeld = true;
    }

    public void eventUnSelect()
    {
        myRB.isKinematic = false;
        transform.SetParent(null);

    }

    public void eventActivate()
    {

    }
}
