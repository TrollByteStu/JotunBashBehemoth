using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfBadMath : MonoBehaviour
{

    float _BadRadius;
    float _BadRadian;
    float _BadAngle;

    float _MyBadRadius;
    float _MyBadRadian;
    float _MyBadAngle;

    BadAngleStruct badStruct = new BadAngleStruct();
    protected struct BadAngleStruct
    {
        public float _BadAngle;
        public float _MyBadAngle;
    }

    protected BadAngleStruct BadAngle(Vector3 vector3)
    {
        _BadRadius = Vector3.Distance(new Vector3(vector3.x , transform.position.y , vector3.z), transform.position);
        _BadRadian = Mathf.Atan2(vector3.x / _BadRadius, vector3.z / _BadRadius);
        _BadAngle = _BadRadian * (180 / Mathf.PI);
        if (_BadAngle < 0)
            _BadAngle += 360f;

        _MyBadRadius = Vector3.Distance(new Vector3(transform.forward.x, transform.position.y, transform.forward.z), transform.position);
        _MyBadRadian = Mathf.Atan2(transform.forward.x / _MyBadRadius, transform.forward.z / _MyBadRadius);
        _MyBadAngle = _MyBadRadian * (180 / Mathf.PI);
        if (_MyBadAngle < 0)
            _MyBadAngle += 360f;

        badStruct._BadAngle = _BadAngle;
        badStruct._MyBadAngle = _MyBadAngle;

        return badStruct;
    }

    protected float LeftOrRightAngle(BadAngleStruct badStruct ,float turnSpeed)
    {
        _BadAngle = badStruct._BadAngle;
        if (_BadAngle > 180)
            _BadAngle -= 360;
        //_BadAngle -= 360;
        _BadAngle -= badStruct._MyBadAngle;

        if (badStruct._BadAngle - badStruct._MyBadAngle < turnSpeed +1
            && badStruct._BadAngle - badStruct._MyBadAngle > -turnSpeed -1)
            return 0;
        else if (_BadAngle < -1)
        {
            //Debug.Log("left");
            return -turnSpeed;
        }
        
        else if (_BadAngle > 1)
        {
           // Debug.Log("right");
            return turnSpeed;
        }
        return 0;

    }



    protected float PlusOrMinus()
    {
        if (Random.Range(1, 2) == 1)
            return 1;
        else
            return -1;
    }

    protected float PlusOrMinus(float f)
    {
        if (Random.Range(1, 2) == 1)
            return f;
        else
            return -f;
    }

    protected int PlusOrMinus(int i)
    {
        if (Random.Range(1, 2) == 1)
            return i;
        else
            return -i;
    }
}
