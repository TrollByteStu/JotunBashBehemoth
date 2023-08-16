using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gannet : MonoBehaviour
{
    // others
    private GameObject _Player;
    private Animator _Animator;

    // landing Curves
    public AnimationCurve _LandingCurve;
    private float _LandingCurveTime;

    // landing stuff
    private Transform _GannetIdlePoints;
    private int _LandingPointInt;
    private bool _ReadyToLand = false;
    private Vector3 _MovePos;
    private float _LandingTime;

    // current state
    public int _CurrentState = 0;

    // orbits
    private bool _RadiusPicked = false;
    private float _Angle;
    private float _Radius;

    // idle
    private float _AttentionTime;
    private Vector3 _LookDirection;
    private float onesec;
    private int frame = 0;

    private void Start()
    {
        _GannetIdlePoints = GameController.Instance._GannetIdlePoints.transform;
        _Player = GameController.Instance.player;
        _Animator = transform.GetComponentInChildren<Animator>();
        if (_Animator == null)
            Debug.LogError("could not find Animator on " + gameObject.name);
    }

    // fly in circles around the raft 
    // dive for fish
    // get killed by player 
    // scream for no reason
    // land on the raft 
    // dive for bait

    void FindLandingPoint()
    {
        _LandingPointInt = Random.Range(1, _GannetIdlePoints.childCount);
        if (_GannetIdlePoints.GetChild(_LandingPointInt).childCount == 0)
        {
            _ReadyToLand = true;
        }
        else
        {
            FindLandingPoint();
        }
    }

    void CircleRaft() // defo not done
    {
        if (!_RadiusPicked)
        {
            _RadiusPicked = true;
            _Radius = Random.Range(5, 10);
        }
        _Angle += Time.fixedDeltaTime /(_Radius / 2);
        transform.LookAt( new Vector3(Mathf.Cos(_Angle) * _Radius, 7, Mathf.Sin(_Angle) * _Radius) , Vector3.up);
        transform.position = new(Mathf.Cos(_Angle) * _Radius, 7, Mathf.Sin(_Angle) * _Radius);
    }

    void GannetLand()
    {
        _LandingCurveTime += Time.fixedDeltaTime;
        if (!_ReadyToLand)
            FindLandingPoint();
        _MovePos = _GannetIdlePoints.GetChild(_LandingPointInt).transform.position;
        _MovePos.y = _GannetIdlePoints.GetChild(_LandingPointInt).transform.position.y + _LandingCurve.Evaluate(_LandingCurveTime);
        transform.position = Vector3.MoveTowards(transform.position, _MovePos, Time.fixedDeltaTime * 3);
        if (Vector3.Distance(transform.position, _MovePos) <= 1)
            GannetLanded();
    }

    void GannetLanded()
    {
        _CurrentState = 3;
        transform.parent = _GannetIdlePoints.GetChild(_LandingPointInt);
        transform.localPosition = Vector3.zero;
        _Animator.SetBool("Idle", true);
        _LandingTime = Time.time;

    }

    void Idle() // need to find a way to slowly rotate it
    {
        if (_AttentionTime + 10 < Time.time)
        {
            _LookDirection = GenerateRandomVector2(-10,10);
            _LookDirection.z = _LookDirection.y;
            _LookDirection.y = transform.position.y;
            //transform.LookAt(_LookDirection, Vector3.up);
            _AttentionTime = Time.time;
        }
        if (onesec + 1 < Time.time)
        {
            onesec = Time.time;
            //Debug.Log(Vector3.Angle(transform.position, _LookDirection) + " " + frame++ + " " + transform.position + " " + _LookDirection);
            //transform.Rotate(Vector3.up , Vector3.Angle(transform.position, _LookDirection));
            //Debug.Log(Vector3.Angle(transform.position, _LookDirection) + " " + frame + " " + transform.position + " " + _LookDirection);
            //Debug.Log(Quaternion.SetLookRotation(_LookDirection));
        }
        

    }

    Vector2 GenerateRandomVector2(float min , float max)
    {
        return new Vector2(Random.Range(min, max), Random.Range(min, max));
    }

    float PlusOrMinus()
    {
        if (Random.Range(1, 2) == 1)
            return 1;
        else
            return -1;
    }
    float PlusOrMinus(float Number)
    {
        if (Random.Range(1, 2) == 1)
            return Number;
        else
            return -Number;
    }

    void FixedUpdate()
    {
        switch (_CurrentState)
        {
            case 1:
                CircleRaft();
                break;
            case 2:
                GannetLand();
                break;
            case 3:
                Idle();
                break;
        }
    }

}
