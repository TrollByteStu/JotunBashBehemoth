using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfBadMath : MonoBehaviour
{
    float _BadAngle;

    protected struct BadAngleStruct
    {
        public float _BadAngle;
        public float _MyBadAngle;
    }

    protected BadAngleStruct BadAngle(Vector3 vector3)
    {
        float radius1 = Vector3.Distance(new Vector3(vector3.x , transform.position.y , vector3.z), transform.position);
        float angle1 = AngleOnCircle(vector3.x, vector3.z, radius1);

        float radius2 = Vector3.Distance(new Vector3(transform.forward.x, transform.position.y, transform.forward.z), transform.position);
        float angle2 = AngleOnCircle(transform.forward.x, transform.forward.z, radius2);

        BadAngleStruct badStruct = new BadAngleStruct();

        badStruct._BadAngle = angle1;
        badStruct._MyBadAngle = angle2;

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

    protected float AngleOnCircle(float x ,float y,float radius)
    {
        float radian = Mathf.Atan2(x / radius, y / radius);
        float angle = radian * (180 / Mathf.PI);
        if (angle< 0)
            angle += 360f;
        return angle;
    }

    protected Vector3 RandomVetor3(float min , float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }
    protected Vector2 RandomVetor2(float min , float max)
    {
        return new Vector2(Random.Range(min, max), Random.Range(min, max));
    }

    protected float PlusOrMinus()
    {
        if (Random.Range(1, 3) == 1)
            return 1;
        else
            return -1;
    }

    protected float PlusOrMinus(float f)
    {
        if (Random.Range(1, 3) == 1)
            return f;
        else
            return -f;
    }

    protected int PlusOrMinus(int i)
    {
        if (Random.Range(1, 3) == 1)
            return i;
        else
            return -i;
    }
}
