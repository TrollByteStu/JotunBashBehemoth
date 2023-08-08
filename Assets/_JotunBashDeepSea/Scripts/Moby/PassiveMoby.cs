using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMoby : MonoBehaviour
{
    private fishBuoyancy[] _Floaters;
    public float _Speed;
    public float _Turn;
    public float _CircleDistance;
    public GameObject _myPlayer;
    public GameObject _Raft;
    public Animator _Animator;
    public float _Spin;
    private Rigidbody _myRB;

    // time stuff
    private float _TimeSinceLastDive;
    private float _RandomTimeAdded;
    private float _TimeSinceSurface;

    // random cords
    public float _DistanceFromRaftMin = 10;
    public float _RandomCordLimit;
    private float _RandomXCord;
    private float _RandomZCord;
    private Vector3 _RandomVector3;

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
        if (_TimeSinceLastDive + _RandomTimeAdded < Time.time)
        {
            GenerateNumbers();
            
        }
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
            MobyEmerge();
        }
    }

    void MobyEmerge()
    {

        _RandomVector3.y = -10;
        transform.position = _RandomVector3;

    }


}
