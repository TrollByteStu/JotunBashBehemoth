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
    private int _CurrentState = 0;

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

    void GannetLand()
    {
        _LandingCurveTime += Time.fixedDeltaTime;
        if (!_ReadyToLand)
            FindLandingPoint();
        _MovePos = _GannetIdlePoints.GetChild(_LandingPointInt).transform.position;
        _MovePos.y = _GannetIdlePoints.GetChild(_LandingPointInt).transform.position.y + _LandingCurve.Evaluate(_LandingCurveTime);
        transform.position = Vector3.MoveTowards(transform.position, _MovePos, Time.fixedDeltaTime * 3);

    }

    void FixedUpdate()
    {
        GannetLand();
    }

}
