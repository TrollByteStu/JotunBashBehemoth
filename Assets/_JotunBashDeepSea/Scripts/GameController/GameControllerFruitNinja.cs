using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerFruitNinja : MonoBehaviour
{
    public int _Score;
    public GameObject _Katana;
    public GameObject _FruitCannon;

    private void Start()
    {
        if (_FruitCannon == null)
            Debug.Log("Fruit ninja does not work without asinged cannon " + this);
    }
}
