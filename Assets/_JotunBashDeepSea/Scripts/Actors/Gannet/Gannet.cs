using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitgem.VFX.StylisedWater;

public class Gannet : InfBadMath
{
    // others
    public GameObject _Prefab;
    public List<Rigidbody> _Rigidbodys;
    private List<Collider> _Colliders = new List<Collider>();
    public Transform _Armature;
    private GameObject _Player;
    private Animator _Animator;
    private AudioSource _AudioSource;
    private OurWateverVolumeFloater _Floater;
    private Bait _BaitScript;

    // landing Curves
    public AnimationCurve _LandingCurve;
    private float _LandingCurveTime;

    // landing stuff
    private Transform _GannetIdlePoints;
    private int _LandingPointInt;
    private bool _ReadyToLand = false;
    private Vector3 _MovePos;
    // current state
    public int _CurrentState = 0;

    // orbits
    private bool _RadiusPicked = false;
    private float _Angle;
    private float _AngleMod;
    private float _Radius;
    private float _FlightTime;
    private float _StartFlightTime;

    // idle
    private float _AttentionTime;
    private Vector3 _LookDirection;
    public AnimationCurve _JumpCurve;
    private float _JumpTime;
    private bool _LookAtPlayer = false;
    private Vector3 _LookReset;
    private bool _DontRotate = false;
    private int _TurnCount;
    private int _TurnCountLimit;

    // screaming
    public int _ScreamingInterval = 10000;
    public int _ScreamCountDown;

    // death
    public bool _Dead = false;
    private bool _InWater = false;
    public bool _Debug = false;

    // bait
    private float _LastBaitCheck;
    private Bait _Bait = null;

    private void Awake()
    {

        _GannetIdlePoints = GameController.Instance._GannetIdlePoints.transform;
        _Player = GameController.Instance.player;
        Rigidbody[] array = GetComponentsInChildren<Rigidbody>();
        _Rigidbodys.AddRange(array);
        foreach (Rigidbody rb in _Rigidbodys)
            rb.isKinematic = true;
        Collider[] colliders = GetComponentsInChildren<Collider>();
        _Colliders.AddRange(colliders);
        _Animator = GetComponentInChildren<Animator>();
        if (_Animator == null)
            Debug.LogError("could not find Animator on " + gameObject.name);
        _AudioSource = GetComponent<AudioSource>();
        if (_AudioSource == null)
            Debug.LogError("could not find AudioSource on " + gameObject.name);
        _ScreamCountDown = _ScreamingInterval;
        _Floater = GetComponent<OurWateverVolumeFloater>();
        _BaitScript = GetComponent<Bait>();

    }

    // fly in circles around the raft 
    // dive for fish
    // get killed by player 
    // scream for no reason
    // land on the raft 
    // dive for bait

    void FindLandingPoint()
    {

        _LandingPointInt = Random.Range(0, _GannetIdlePoints.childCount);
        if (_GannetIdlePoints.GetChild(_LandingPointInt).childCount == 0)
        {
            _ReadyToLand = true;
        }
        else if (Random.Range(1,10) == 1)
        {
            FindLandingPoint();
        }
    }

    void CircleRaft()
    {
        if (!_RadiusPicked)
        {
            _RadiusPicked = true;
            _Radius = Random.Range(5, 10);
            _StartFlightTime = Time.time;
            _FlightTime = Random.Range(10f, 30f);
            _AngleMod = PlusOrMinus();
            _Angle = Random.Range(1, 361);
        }
        if (_StartFlightTime + _FlightTime < Time.time)
            _CurrentState = 2;
        _Angle += (Time.fixedDeltaTime /(_Radius / 2)) * _AngleMod;
        _MovePos = new(Mathf.Cos(_Angle) * _Radius, 7, Mathf.Sin(_Angle) * _Radius);
        transform.LookAt(_MovePos , Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, _MovePos,Time.fixedDeltaTime * 4);
    }

    void GetBait()
    {
        if (_Bait == null)
            _CurrentState = 1;
        transform.position = Vector3.Slerp(transform.position, _Bait.transform.position, Time.fixedDeltaTime);
        transform.LookAt(Vector3.Slerp(transform.position, _Bait.transform.position, Time.fixedDeltaTime));
        if (Vector3.Distance(transform.position, _Bait.transform.position) <= 0.5f)
        {
            _CurrentState = 1;
            Destroy(_Bait.gameObject);
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
        transform.LookAt(Vector3.MoveTowards(transform.position, _MovePos, Time.fixedDeltaTime * 3));
        if (_GannetIdlePoints.GetChild(_LandingPointInt).childCount != 0)
        {
            _CurrentState = 1;
            _RadiusPicked = false;
        }
        
        if (Vector3.Distance(transform.position, _GannetIdlePoints.GetChild(_LandingPointInt).transform.position) <= 1)
            GannetLanded();
    }

    void GannetLanded()
    {
        _CurrentState = 3;
        transform.parent = _GannetIdlePoints.GetChild(_LandingPointInt);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        _Animator.SetBool("Idle", true);
        _MovePos.x = 0;
        _MovePos.z = 0;
        _TurnCountLimit = Random.Range(2, 10);
        _TurnCount = 0;
    }

    void Idle() // done
    {
        if (_AttentionTime + 10 < Time.time)
        {
            _LookReset = transform.forward;
            _LookReset.y = transform.position.y;
            transform.LookAt(_LookReset);
            _DontRotate = false;
            if (Random.Range(1,5) >= 3)
            {
                _LookDirection = GenerateRandomVector2(-10,10);
                _LookDirection.z = _LookDirection.y;
                _LookDirection.y = transform.position.y;
                _LookAtPlayer = false;
            }
            else
            {
                _LookAtPlayer = true;
                _LookDirection = _Player.transform.position;
            }
            if (_TurnCount++ == _TurnCountLimit)
            {
                _Animator.SetBool("Idle", false);
                _RadiusPicked = false;
                _CurrentState = 1;
                transform.SetParent(null);
                return;
            }
            _AttentionTime = Time.time;
        }

        if (LeftOrRightAngle(BadAngle(_LookDirection), 3) != 0 && !_DontRotate)
        {
            _JumpTime += Time.fixedDeltaTime;
            transform.Rotate(Vector3.up, LeftOrRightAngle(BadAngle(_LookDirection), 3));
            _MovePos.y = _JumpCurve.Evaluate(_JumpTime);
            transform.localPosition = _MovePos;

        }
        else if (_LookAtPlayer && !_DontRotate)
        {
            transform.LookAt(_Player.transform.position);
            _LookAtPlayer = false;
            _DontRotate = true;
        }
        else
        {
            _JumpTime = 0;
            transform.localPosition = Vector3.zero;
        }

    }

    Vector2 GenerateRandomVector2(float min , float max)
    {
        return new Vector2(Random.Range(min, max), Random.Range(min, max));
    }

    public void OnDeath()
    {
        GameController.Instance._GannetHandler.KillGannet(gameObject);
        _AudioSource.clip = GameController.Instance._GannetHandler._PainSounds[Random.Range(0, GameController.Instance._GannetHandler._PainSounds.Count)];
        _AudioSource.Play();
        _Dead = true;
        _Animator.enabled = false;
        foreach (Rigidbody rb in _Rigidbodys)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward);
        }
        transform.SetParent(null);
        Destroy(gameObject, 20);

    }

    public void OnExplosion()
    {
        GameController.Instance._GannetHandler.KillGannet(gameObject);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (!_Dead)
        {
            if ( Random.Range(1,_ScreamCountDown--) == 1 && !_AudioSource.isPlaying)
            {
                _AudioSource.clip = GameController.Instance._GannetHandler._RandomSounds[Random.Range(0, GameController.Instance._GannetHandler._RandomSounds.Count)];
                _AudioSource.Play();
                _ScreamCountDown = _ScreamingInterval;
            }
            switch (_CurrentState)
            {
                case 1:
                    CircleRaft();
                    if (_LastBaitCheck + 10 < Time.time)
                    {
                        _LastBaitCheck = Time.time;
                        _Bait = GameController.Instance.checkForBaits(_Prefab, transform);
                        if (_Bait != null)
                        {
                            _CurrentState = 4;
                        }
                    }
                    break;
                case 2:
                    GannetLand();
                    break;
                case 3:
                    Idle();
                    break;
                case 4:
                    GetBait();
                    break;
                    
            }
        }
        else if (_Dead && !_Debug)
        {
            _Debug = true;
            OnDeath();
        }
        else if (_Dead && _InWater)
        {
            _Armature.rotation = Quaternion.Slerp(_Armature.rotation, Quaternion.LookRotation(_Armature.forward, Vector3.left), Time.deltaTime);
        }

        if (transform.position.y <= -5)
            Destroy(gameObject);
    }

    public void OnWaterEnter(GannetEnterWater script)
    {
        if (_Dead)
        {
            _Armature.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _Floater.enabled = true;
            _BaitScript.floating = true;
            _BaitScript.ActivateBait();
            _Armature.localPosition = Vector3.zero;
            _InWater = true;
            Destroy(script);
        }
    }
}
