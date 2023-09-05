using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public bool _BeingHeld = false;
    public bool _Touched = false;

    public float _CurrentTimeScale = 1;
    public float _MinTimeScale;
    public bool _SlowTime;

    private float _DeleteTime;
    private Rigidbody _Rigidbody;

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_SlowTime)
        {
            _CurrentTimeScale -= Time.unscaledDeltaTime / 2;
            _CurrentTimeScale = Mathf.Clamp(_CurrentTimeScale, _MinTimeScale, 1);
            Time.timeScale = _CurrentTimeScale;
        }
        else
        {
            _CurrentTimeScale += Time.unscaledDeltaTime / 2;
            _CurrentTimeScale = Mathf.Clamp(_CurrentTimeScale, _MinTimeScale, 1);
            Time.timeScale = _CurrentTimeScale;
        }

        if (_DeleteTime + 10 < Time.time && !_BeingHeld && _Touched)
            Destroy(gameObject);

        if (transform.position.y <= -10)
            Destroy(gameObject);

    }
     
    public void eventSelect()
    {
        _BeingHeld = true;
        _Touched = true;
        if (_SlowTime)   
            _SlowTime = false;
    }

    public void eventDeselect()
    {
        _DeleteTime = Time.time;
        _BeingHeld = false;
        _Rigidbody.isKinematic = false;
        transform.SetParent(null);
        if (_SlowTime)
            _SlowTime = false;
    }

    public void eventActivate()
    {
        _SlowTime = true;
    }

    public void eventDeactivate()
    {
        _SlowTime = false;
    }
}
