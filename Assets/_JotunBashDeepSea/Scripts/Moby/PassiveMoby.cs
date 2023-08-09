using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMoby : MonoBehaviour
{
    private fishBuoyancy[] _Floaters;
    public GameObject _myPlayer;
    public GameObject _Raft;
    public Animator _Animator;
    private Rigidbody _myRB;

    // time stuff
    private float _TimeSinceSurface;
    public float _SurfaceTime;
    private float _TimeSinceDive;
    public float _DiveTime;
    private float _RandomTimeAdded;
    public float _RandomTimeAddedMax;
    

    // random cords
    public float _DistanceFromRaftMin = 10;
    public float _RandomCordLimit;
    private float _RandomXCord;
    private float _RandomZCord;
    private Vector3 _RandomVector3;

    // circle stuff
    private float _Radius;
    private float _Radian;
    private float _Angle;
    public float _AngleChangeSpeed = 1;
    private float _AngleDirectionMod;
    private Vector3 _MobyMovePoint;


    // bools and stuff
    private bool _Emerging = true;

    // animation curves
    private float _AnimationTimer;
    public AnimationCurve _EmergingCurve;

    void Start()
    {
        _Floaters = transform.GetComponentsInChildren<fishBuoyancy>();
        _myRB = GetComponent<Rigidbody>();
        if (_myPlayer == null)
            _myPlayer = GameController.Instance.player;
        GenerateCords();
    }
        // generate 2 random number on a grid 
        // check if the numbers are too close to player
        // go back to start 
        // else place moby close below grid point 
        // stay for some time 
        // if moby hit with in the 
        // scream and swim away 
        // else swim away 
        // if hit n amount of times 
        // moby rushes player and play bite animation
        // trigger level change

    private void FixedUpdate()
    {
        if (_Emerging)
            MobyEmerge();
        else
            MobySink();
    }

    void GenerateCords()
    {
        _RandomXCord = Random.Range(-_RandomCordLimit, _RandomCordLimit);
        _RandomZCord = Random.Range(-_RandomCordLimit, _RandomCordLimit);
        CheckDistanceFromPlayer();
    }

    void CheckDistanceFromPlayer()
    {
        _RandomVector3 = new(_RandomXCord, 0f, _RandomZCord);
        if (Vector3.Distance(_RandomVector3 + _Raft.transform.position, _myPlayer.transform.position) < _DistanceFromRaftMin)
        {
            GenerateCords();
        }
        else
        {
            PlaceMoby();
        }
    }
    void PlaceMoby()
    {
        _RandomVector3.y = -10;
        transform.position = _RandomVector3;
        transform.LookAt(new Vector3(_myPlayer.transform.position.x, transform.position.y, _myPlayer.transform.position.z));
        transform.Rotate(Vector3.up, 90 * PlusOrMinus());
        transform.position += transform.forward * -10;
        _Radius = Vector3.Distance(new Vector3(_RandomXCord, _Raft.transform.position.y, _RandomZCord), _Raft.transform.position);
        _Radian = Mathf.Atan2(_RandomZCord / _Radius, _RandomXCord / _Radius);
        _Angle = _Radian * (180 / Mathf.PI);
        if (_Angle < 0)
            _Angle += 360f;
        _AngleDirectionMod = PlusOrMinus();
        _TimeSinceSurface = Time.time;
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

    void MobyEmerge()
    {
        _AnimationTimer += Time.deltaTime;
        _Angle += (Time.fixedDeltaTime * _AngleChangeSpeed * _AngleDirectionMod) / _Radius;
        _MobyMovePoint = new Vector3(Mathf.Sin(_Angle) * _Radius, _EmergingCurve.Evaluate(_AnimationTimer), Mathf.Cos(_Angle) * _Radius);
        transform.LookAt(_MobyMovePoint);
        transform.position = _MobyMovePoint;
        if (_TimeSinceSurface + _SurfaceTime < Time.time)
        {
            MobyDive();
        }

    }

    void MobyDive()
    {
        _Emerging = false;
        _AnimationTimer = _EmergingCurve[_EmergingCurve.length - 1].time;
        _TimeSinceDive = Time.time;
        _RandomTimeAdded = Random.Range(0f, _RandomTimeAddedMax);
    }

    void MobySink()
    {
        _AnimationTimer -= Time.deltaTime;
        _Angle += (Time.fixedDeltaTime * _AngleChangeSpeed * _AngleDirectionMod) / _Radius;
        _MobyMovePoint = new Vector3(Mathf.Sin(_Angle) * _Radius, _EmergingCurve.Evaluate(_AnimationTimer), Mathf.Cos(_Angle) * _Radius);
        transform.LookAt(_MobyMovePoint);
        transform.position = _MobyMovePoint;
        if (_TimeSinceDive + _DiveTime + _RandomTimeAdded < Time.time)
        {
            _Emerging = true;
            GenerateCords();
            _AnimationTimer = 0;
        }

    }


}
