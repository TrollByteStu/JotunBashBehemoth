using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaBlade : MonoBehaviour
{
    Vector3 _LastPos;
    public float _Magnitude;
    AudioSource _AudioSource;
    BoxCollider _BoxCollider;

    Vector3 _Forward;
    Vector3 _Backward;
    float _ForwardDistance;
    float _BackwardDistance;
    // Start is called before the first frame update
    void Start()
    {
        _BoxCollider = GetComponent<BoxCollider>();
        _LastPos = transform.position;
        if (GetComponent<AudioSource>())
            _AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _ForwardDistance = Vector3.Distance(_Forward, transform.position);
        _BackwardDistance = Vector3.Distance(_Backward, transform.position);
        _Magnitude = Vector3.Distance(_LastPos, transform.position);
        _LastPos = transform.position;
        _Forward = transform.forward + transform.position;
        _Backward = -transform.forward + transform.position;

        if (_ForwardDistance < _BackwardDistance && _Magnitude >= 0.2f)
        {
            if (!_AudioSource.isPlaying && _AudioSource != null)
                _AudioSource.Play();
            _BoxCollider.isTrigger = true;
        }
        else
            _BoxCollider.isTrigger = false;
    }
}
