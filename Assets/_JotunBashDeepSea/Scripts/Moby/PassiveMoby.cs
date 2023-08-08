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
    private float _TimeSinceLastDive;
    private float _RandomTimeAdded;
    private float _TimeSinceSurface;

    // random cords
    public float _DistanceFromRaftMin = 10;
    public float _RandomCordLimit;
    public float _RandomXCord;
    public float _RandomZCord;
    private Vector3 _RandomVector3;
    public float _Radius;

    // bools and stuff
    private bool _Emerging = true;
    private bool _MobyPlaced = false;



    // animation curves
    private float _AnimationTimer;
    public AnimationCurve _EmergingCurve;

    void Start()
    {
        _Floaters = transform.GetComponentsInChildren<fishBuoyancy>();
        _myRB = GetComponent<Rigidbody>();
        if (_myPlayer == null)
            _myPlayer = GameController.Instance.player;
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
        _AnimationTimer += Time.deltaTime;
        if (_TimeSinceLastDive + _RandomTimeAdded < Time.time && !_MobyPlaced)
            GenerateNumbers();
        
        if (_Emerging)
            MobyEmerge();
    }

    void GenerateNumbers()
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
            GenerateNumbers();
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
        _MobyPlaced = true;
        Debug.Log("x = " + _RandomXCord / _Radius + "z = " + _RandomZCord / _Radius);
    }

    float PlusOrMinus()
    {
        if (Random.Range(1, 2) == 1)
            return 1;
        else
            return -1;
    }

    void MobyEmerge()
    {
        //transform.LookAt(new Vector3(transform.position.x, _EmergingCurve.Evaluate(_AnimationTimer), transform.position.z) , Vector3.up);
        //transform.position = new Vector3()
        transform.position = new Vector3(transform.position.x ,_EmergingCurve.Evaluate(_AnimationTimer),transform.position.z);


    }


}