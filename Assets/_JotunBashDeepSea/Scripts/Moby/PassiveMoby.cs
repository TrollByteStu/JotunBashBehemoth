using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassiveMoby : InfBadMath
{
    public GameObject _myPlayer;
    public GameObject _Raft;
    public Animator _Animator;
    public GameObject _Lookat;

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
    private bool _MobyPlaced = false;

    // animation curves
    private float _AnimationTimer;
    public AnimationCurve _EmergingCurve;

    // hits and suff
    public int _HitPoints = 3;
    private bool _Invulnerable = false;

    // game controller
    private GameController mainGC;

    // blowhole logic
    public Transform blowHoleTransform;
    public bool blowDisabled = false;

    // sounds
    public AudioClip MobyDickAttackSound;
    private AudioSource myAS;

    void Start()
    {
        if (_myPlayer == null)
            _myPlayer = GameController.Instance.player;
        if (_Raft == null)
            _Raft = GameController.Instance.BoatRig;
        GenerateCords(0);
        mainGC = GameController.Instance;
        myAS = GetComponent<AudioSource>();
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

    private void Update()
    {
        //if (_HitPoints <= 0 && _TimeSinceDive + _DiveTime + _RandomTimeAdded < Time.time && _Emerging)
        if (GameController.Instance.secondsLeft < 0f && _TimeSinceDive + _DiveTime < Time.time && _Emerging)
        {
            _HitPoints = -1;
            MobySceneChange();
        }
        else if (_Emerging)
            MobyEmerge();
        else
            MobySink();
    }

    void GenerateCords(float number)
    {
        _RandomVector3.x = Random.Range(-(_RandomCordLimit + number), _RandomCordLimit + number);
        _RandomVector3.z = Random.Range(-(_RandomCordLimit + number), _RandomCordLimit + number);
        CheckDistanceFromPlayer(number);
    }

    void CheckDistanceFromPlayer(float number)
    {
        if (Vector3.Distance(_RandomVector3 + _Raft.transform.position, _myPlayer.transform.position) < _DistanceFromRaftMin)
        {
            GenerateCords(number);
        }
        else if (_HitPoints >= 0)
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
        _Radius = Vector3.Distance(new Vector3(_RandomVector3.x, _Raft.transform.position.y, _RandomVector3.z), _Raft.transform.position);
        _Radian = Mathf.Atan2(_RandomVector3.z / _Radius, _RandomVector3.x / _Radius);
        _Angle = _Radian * (180 / Mathf.PI);
        if (_Angle < 0)
            _Angle += 360f;
        _AngleDirectionMod = PlusOrMinus();
        _TimeSinceSurface = Time.time;
    }

    void MobyBlow()
    { // when whale surfaces, spawn blow particles and wait before it will do this again..
        blowDisabled = true;
        var blowEffect = Instantiate(mainGC.gcResources.BlowHoleSprays[0], blowHoleTransform.position, blowHoleTransform.rotation, blowHoleTransform);
        Destroy(blowEffect, 10f);
        GameController.Instance.gcNarrator.Tell("MobyDick");
    }

    void MobyEmerge()
    {
        _AnimationTimer += Time.deltaTime;
        _Angle += (Time.deltaTime * _AngleChangeSpeed * _AngleDirectionMod) / _Radius;
        _MobyMovePoint = new Vector3(Mathf.Sin(_Angle) * _Radius, _EmergingCurve.Evaluate(_AnimationTimer), Mathf.Cos(_Angle) * _Radius);
        _Lookat.transform.LookAt(_MobyMovePoint);
        transform.rotation = Quaternion.Lerp(transform.rotation, _Lookat.transform.rotation, Time.deltaTime);
        transform.position = _MobyMovePoint;
        if (!blowDisabled && transform.position.y > -2.5f) MobyBlow();
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
        blowDisabled = false;
        _AnimationTimer -= Time.deltaTime;
        _Angle += (Time.deltaTime * _AngleChangeSpeed * _AngleDirectionMod) / _Radius;
        _MobyMovePoint = new Vector3(Mathf.Sin(_Angle) * _Radius, _EmergingCurve.Evaluate(_AnimationTimer), Mathf.Cos(_Angle) * _Radius);
        _Lookat.transform.LookAt(_MobyMovePoint);
        transform.rotation = Quaternion.Lerp(transform.rotation, _Lookat.transform.rotation,Time.deltaTime);
        transform.position = _MobyMovePoint;
        if (_TimeSinceDive + _DiveTime + _RandomTimeAdded < Time.time)
        {
            _Emerging = true;
            _Invulnerable = false;
            GenerateCords(0);
            _AnimationTimer = 0;
        }
    }

    void PlaceMobyEnding()
    {
        GenerateCords(20);
        _RandomVector3.y = -10;
        transform.position = _RandomVector3;
        transform.LookAt(new Vector3(_Raft.transform.position.x, transform.position.y, _Raft.transform.position.z));
        transform.position += transform.forward * -10;
        _MobyPlaced = true;
        _AnimationTimer = 0;
        myAS.clip = MobyDickAttackSound;
        myAS.volume = 1f;
        myAS.Play();
    }

    void MobySceneChange()
    {
        if (!_MobyPlaced)
            PlaceMobyEnding();
        _AnimationTimer += Time.deltaTime;
        _MobyMovePoint = new Vector3(Vector3.MoveTowards(transform.position,_Raft.transform.position, Time.deltaTime * 3).x, _EmergingCurve.Evaluate(_AnimationTimer), Vector3.MoveTowards(transform.position, _Raft.transform.position, Time.deltaTime * 3).z);
        _Lookat.transform.LookAt(_MobyMovePoint);
        transform.rotation = Quaternion.Lerp(transform.rotation, _Lookat.transform.rotation, Time.deltaTime);
        transform.position = _MobyMovePoint;
        if (Vector3.Distance(transform.position, _Raft.transform.position) <= 23f)
        {
            _Animator.SetTrigger("Bite");
        }

        if (Vector3.Distance(transform.position, _Raft.transform.position) <= 12f)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void MobyHit()
    {
        if (!_Invulnerable && _HitPoints > 0)
        {
            _Invulnerable = true;
            MobyDive();
            _HitPoints--;
        }
    }

    public void MobyHit(float hitNumber)
    {
        if (!_Invulnerable && _HitPoints > 0)
        {
            _Invulnerable = true;
            MobyDive();
            _HitPoints -= (int)hitNumber;
        }
    }

    public void MobyHit(int hitNumber)
    {
        if (!_Invulnerable && _HitPoints > 0)
        {
            _Invulnerable = true;
            MobyDive();
            _HitPoints -= hitNumber;
        }
    }

    public void MobyAnger()
    {
        MobyDive();
        _HitPoints = 0;
    }


}
