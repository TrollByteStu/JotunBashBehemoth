using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public bool _BeingHeld = false;
    public bool _Touched = false;

    private bool _SlowTime;
    private float _DeleteTime;
    private Rigidbody _Rigidbody;
    private GameControllerFruitNinja _GcFruitNinja;
    private AudioSource _AudioSource;

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        _AudioSource = GetComponent<AudioSource>();
        _GcFruitNinja = GameController.Instance.gcFruitNinja;
    }

    private void Update()
    {
        if (_DeleteTime + 10 < Time.time && !_BeingHeld && _Touched)
        {
            _GcFruitNinja._Katana.Remove(gameObject);
            Destroy(gameObject);
        }

        if (transform.position.y <= -10)
        {
            _GcFruitNinja._Katana.Remove(gameObject);
            Destroy(gameObject);
        }
    }
     
    public void eventSelect()
    {
        if (!_Touched)
        {
            _GcFruitNinja._Katana.Add(gameObject);
            _AudioSource.Play();
            _GcFruitNinja._On = true;
            _GcFruitNinja._Stage = 0;
        }
        _BeingHeld = true;
        _Touched = true;
    }

    public void eventDeselect()
    {
        _DeleteTime = Time.time;
        _BeingHeld = false;
        _Rigidbody.isKinematic = false;
        transform.SetParent(null);
        if (_SlowTime)
        {
            _GcFruitNinja._SlowTimeBool -= 1;
            _SlowTime = false;
        }
    }

    public void eventActivate()
    {
        _SlowTime = true;
        _GcFruitNinja._SlowTimeBool += 1;
    }

    public void eventDeactivate()
    {
        _SlowTime = false;
        _GcFruitNinja._SlowTimeBool -= 1;
    }
}
