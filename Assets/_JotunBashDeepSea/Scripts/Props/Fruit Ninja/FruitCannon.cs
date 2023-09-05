using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCannon : MonoBehaviour
{
    public Transform _PitchTransform;
    public Transform _CannonHole;
    public Transform cube;

    void Update()
    {
        transform.LookAt(cube);
        _PitchTransform.LookAt(cube);

    }
}
