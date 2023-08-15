using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gannet : MonoBehaviour
{
    // landing Curves
    public AnimationCurve _LandingCurve;
    private float _LandingCurveTime;

    // landing stuff
    private Transform _GannetIdlePoints;
    private int _LandingPointInt;
    public bool _ReadyToLand = false;
    private Vector3 _MovePos;

    // current state
    public int _CurrentState = 0;

    // orbits
    private bool _RadiusPicked = false;
    private float _Angle;
    private float _Radius;

    private void Start()
    {
        _GannetIdlePoints = GameController.Instance._GannetIdlePoints.transform;
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
    }

    void Idle()
    {

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
