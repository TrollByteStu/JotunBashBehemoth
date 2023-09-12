using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public bool _BeingHeld = false;
    public bool _Touched = false;

    private float _DeleteTime;
    private Rigidbody _Rigidbody;
    private GameControllerFruitNinja _GcFruitNinja;

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        _GcFruitNinja = GameController.Instance.gcFruitNinja;
        _GcFruitNinja._Katana = gameObject;
    }

    private void Update()
    {
        if (_DeleteTime + 10 < Time.time && !_BeingHeld && _Touched)
        {
            _GcFruitNinja._On = false;
            _GcFruitNinja._Stage = 0;
            Destroy(gameObject);
        }

        if (transform.position.y <= -10)
        {
            _GcFruitNinja._On = false;
            _GcFruitNinja._Stage = 0;
            Destroy(gameObject);
        }
    }
     
    public void eventSelect()
    {
        _GcFruitNinja._On = true;
        _GcFruitNinja._Stage = 0;
        _BeingHeld = true;
        _Touched = true;
        if (_GcFruitNinja._SlowTime)
            _GcFruitNinja._SlowTime = false;
    }

    public void eventDeselect()
    {
        _DeleteTime = Time.time;
        _BeingHeld = false;
        _Rigidbody.isKinematic = false;
        transform.SetParent(null);
        if (_GcFruitNinja._SlowTime)
            _GcFruitNinja._SlowTime = false;
    }

    public void eventActivate()
    {
        _GcFruitNinja._SlowTime = true;
    }

    public void eventDeactivate()
    {
        _GcFruitNinja._SlowTime = false;
    }
}
