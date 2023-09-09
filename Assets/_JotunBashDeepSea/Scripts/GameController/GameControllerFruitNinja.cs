using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerFruitNinja : MonoBehaviour
{
    public bool _On;
    public int _Score = 0;
    public int _Stage = 0;
    public GameObject _Katana;
    public GameObject _FruitCannon1;
    public GameObject _FruitCannon2;
    public GameObject _FruitCannon3;

    public bool _SlowTime;
    public float _MinTimeScale = 0.5f;
    private float _CurrentTimeScale;

    private void Start()
    {
        if (_FruitCannon1 == null)
            Debug.Log("Fruit ninja does not work without asinged cannon " + this);
        if (_FruitCannon2 == null)
            Debug.Log("Fruit ninja does not work without asinged cannon " + this);
        if (_FruitCannon3 == null)
            Debug.Log("Fruit ninja does not work without asinged cannon " + this);
    }

    private void Update()
    {
        if (_On)
        {
            switch (_Stage)
            {
                case 0:
                    _FruitCannon1.SetActive(true);
                    _FruitCannon1.GetComponent<FruitCannon>()._FireRate = 1;
                    _Score = 0;
                    _Stage = 1;
                    break;
                case 1:
                    if (_Score >= 20)
                    {
                        _FruitCannon2.SetActive(true);
                        _FruitCannon2.GetComponent<FruitCannon>()._FireRate = 1f;
                        _Stage = 2;
                    }
                    break;
                case 2:
                    if (_Score >= 40)
                    {
                        _FruitCannon3.SetActive(true);
                        _FruitCannon3.GetComponent<FruitCannon>()._FireRate = 1f;
                        _Stage = 3;
                    }
                    break;
                case 3:
                    if (_Score >= 60)
                    {
                        _FruitCannon1.GetComponent<FruitCannon>()._FireRate = 0.5f;
                        _FruitCannon2.GetComponent<FruitCannon>()._FireRate = 0.5f;
                        _FruitCannon3.GetComponent<FruitCannon>()._FireRate = 0.5f;
                        _Stage = 4;
                    }
                    break;
            } 
            TimeScale();
        }
        else
        {
            _FruitCannon1.SetActive(false);
            _FruitCannon2.SetActive(false);
            _FruitCannon3.SetActive(false);
        }
    }

    void TimeScale()
    {
        if (_SlowTime)
        {
            _CurrentTimeScale -= Time.unscaledDeltaTime / 2;
            _CurrentTimeScale = Mathf.Clamp(_CurrentTimeScale, _MinTimeScale, 1);
            Time.timeScale = _CurrentTimeScale;
        }
        else
        {
            _CurrentTimeScale += Time.unscaledDeltaTime / 2;
            _CurrentTimeScale = Mathf.Clamp(_CurrentTimeScale, _MinTimeScale, 1);
            Time.timeScale = _CurrentTimeScale;
        }
    }

}
