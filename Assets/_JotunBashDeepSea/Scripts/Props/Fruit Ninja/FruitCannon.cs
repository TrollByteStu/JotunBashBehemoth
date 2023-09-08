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
    private float _Distance; // distance from target

    // firing 
    public Transform _CannonHole;
    public GameObject[] _Fruits;
    public float _FireRate = 1;
    private float _LastShot;
    public float _FruitSpeed;
    public Vector2 _RandomOffset; // for min and max of randomOffset vector3
    private Vector3 _RandomOffset1; // actual offset
    

    void Update()
    {
        LookAtVector(Target.position + _RandomOffset1);

        if (_LastShot + _FireRate < Time.time)
        {
            _LastShot = Time.time;
            FireFruit();
            _RandomOffset1 = RandomVetor3(_RandomOffset.x,_RandomOffset.y);
            _Distance = Vector3.Distance(_PitchTransform.position, Target.transform.position);
            
        }
    }

    void LookAtVector(Vector3 vector3)
    {   
        _YawLookat.LookAt(new Vector3(vector3.x, _YawTransform.position.y, vector3.z));
        _pitchLookat.LookAt(vector3);


        _PitchTransform.rotation = Quaternion.Lerp(_PitchTransform.rotation, _pitchLookat.rotation,Time.deltaTime * 2);
        _YawTransform.rotation = Quaternion.Lerp(_YawTransform.rotation, _YawLookat.rotation,Time.deltaTime * 2);
    }

    bool _Alined1 = false;
    bool _Alined2 = false;

    void ArcShot(Vector3 vector3)
    {
        // pick a vector3 and lookat it
        // get distace from vector3
        // calculate angle of fire based on speed and distance
        // rotate cannon x axis
        // fire
        if (!_Alined1)
        {
            _YawLookat.LookAt(new Vector3(vector3.x, _YawTransform.position.y, vector3.z));
            _pitchLookat.LookAt(vector3);


            _PitchTransform.rotation = Quaternion.Lerp(_PitchTransform.rotation, _pitchLookat.rotation, Time.deltaTime * 2);
            _YawTransform.rotation = Quaternion.Lerp(_YawTransform.rotation, _YawLookat.rotation, Time.deltaTime * 2);
            //if alinged
            // _Alined1 = true;
        }
        else if (_Alined2)
        {

        }






    }

    void FireFruit()
    {
        var fruit = Instantiate(_Fruits[Random.Range(0,_Fruits.Length)],_CannonHole.position,_CannonHole.rotation);
        fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.forward * _FruitSpeed);
        Destroy(fruit, 5);
    }

}
