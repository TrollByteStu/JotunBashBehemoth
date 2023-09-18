using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCannon : InfBadMath
{

    // debuging
    public Transform Target;

    // cannon movement

    public Transform _YawTransform; // body transfrom
    public Transform _PitchTransform; // barrel transform
    public Transform _YawLookat;
    public Transform _pitchLookat;

    // firing 
    private AudioSource _AudioSource;
    public Transform _CannonHole;
    public GameObject[] _Fruits;
    public float _FireRate = 1;
    public float _LastShot;
    [Range(0,1000)]
    public float _FruitSpeed;
    public Vector2 _RandomOffset; // for min and max of randomOffset vector3
    [Range(-10,10)]
    public float _PlayerYOffset;
    private Vector3 _RandomOffset1; // actual offset

    // math
    private float _DistanceXZ; // distance from target XZ
    private float _DistanceY; // distance from target Y
    private float _G; // gravity
    private float _InsideSqrt;
    private float _Theta;
    private float _Degrees;
    private float _V; // convert force to m/s

    private void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        Target = GameController.Instance.gcFruitNinja._Target;
        _G = -Physics.gravity.y; // should be -9.81 so conveted to 9.81
        _RandomOffset1 = RandomVetor3(_RandomOffset.x, _RandomOffset.y);
        _DistanceXZ = Vector3.Distance(_PitchTransform.position, new Vector3(Target.position.x, _PitchTransform.position.y, Target.position.z));
        _DistanceY = Vector3.Distance(_PitchTransform.position, new Vector3(_PitchTransform.position.x, Target.position.y, _PitchTransform.position.z));
        _DistanceY += _PlayerYOffset;
        FireAngle();
    }

    void Update()
    {
        ArcShot(Target.position + _RandomOffset1);
        
        if (_LastShot + _FireRate < Time.time)
        {
            _LastShot = Time.time;
            FireFruit();
            _RandomOffset1 = RandomVetor3(_RandomOffset.x,_RandomOffset.y);
            _DistanceXZ = Vector3.Distance(_PitchTransform.position, new Vector3(Target.position.x,_PitchTransform.position.y,Target.position.z));
            _DistanceY = Target.position.y - _PitchTransform.position.y +_PlayerYOffset;
            FireAngle();
            
        }
    }

    void LookAtVector(Vector3 vector3)
    {   
        _YawLookat.LookAt(new Vector3(vector3.x, _YawTransform.position.y, vector3.z));
        _pitchLookat.LookAt(vector3);


        _PitchTransform.rotation = Quaternion.Lerp(_PitchTransform.rotation, _pitchLookat.rotation,Time.deltaTime * 2);
        _YawTransform.rotation = Quaternion.Lerp(_YawTransform.rotation, _YawLookat.rotation,Time.deltaTime * 2);
    }

    void ArcShot(Vector3 vector3)
    {
        // pick a vector3 and lookat it
        // get distace from vector3
        // calculate angle of fire based on speed and distance
        // rotate cannon x axis
        // fire
        _YawLookat.LookAt(new Vector3(vector3.x, _YawTransform.position.y, vector3.z));
        _pitchLookat.LookAt(new Vector3(vector3.x, _pitchLookat.position.y, vector3.z));
        _pitchLookat.Rotate(_pitchLookat.transform.forward, _Degrees);
        
        _PitchTransform.rotation = Quaternion.Lerp(_PitchTransform.rotation, _pitchLookat.rotation, Time.deltaTime * 2);
        _YawTransform.rotation = Quaternion.Lerp(_YawTransform.rotation, _YawLookat.rotation, Time.deltaTime * 2);
    }


    //https://en.wikipedia.org/wiki/Projectile_motion

    void FireAngle()
    {   // force / weight(mass * gravity) i dont know why it works when i add a X 5 but it does
        _V = _FruitSpeed / (0.5f * _G * 5);
        _InsideSqrt = Mathf.Sqrt(Mathf.Pow(_V, 4) - _G * (_G * Mathf.Pow(_DistanceXZ, 2) + 2 * (_DistanceY * Mathf.Pow(_DistanceY,2))));
        _Theta = Mathf.Atan((Mathf.Pow(_V, 2) - _InsideSqrt) / (_G * _DistanceXZ));
        _Degrees = _Theta * (180 / Mathf.PI);
    }

    void FireFruit()
    {
        _AudioSource.Play();
        var fruit = Instantiate(_Fruits[Random.Range(0,_Fruits.Length)],_CannonHole.position,_CannonHole.rotation);
        fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.forward * _FruitSpeed);
        Destroy(fruit, 5);
    }

}
