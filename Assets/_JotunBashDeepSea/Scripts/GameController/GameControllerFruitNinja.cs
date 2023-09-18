using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerFruitNinja : MonoBehaviour
{
    public int _Score = 0;
    public int _Stage = 0;
    public Transform _Target;
    public List<GameObject> _Katana;
    public GameObject _FruitCannons;
    public TMP_Text _ScoreText;
    private GameObject _FruitCannon1;
    private GameObject _FruitCannon2;
    private GameObject _FruitCannon3;
    private FruitCannon _FruitCannonScript1;
    private FruitCannon _FruitCannonScript2;
    private FruitCannon _FruitCannonScript3;


    public int _SlowTimeBool;
    public float _MinTimeScale = 0.5f;
    public float _CurrentTimeScale;

    private void Start()
    {
        _Target = Camera.main.transform;

        if (_FruitCannons != null)
        {
            if (_ScoreText == null)
                _ScoreText = _FruitCannons.GetComponentInChildren<TMP_Text>();

            _FruitCannon1 = _FruitCannons.transform.GetChild(0).gameObject;
            _FruitCannonScript1 = _FruitCannon1.GetComponent<FruitCannon>();
            _FruitCannonScript1.Target = _Target;

            _FruitCannon2 = _FruitCannons.transform.GetChild(1).gameObject;
            _FruitCannonScript2 = _FruitCannon2.GetComponent<FruitCannon>();
            _FruitCannonScript2.Target = _Target;

            _FruitCannon3 = _FruitCannons.transform.GetChild(2).gameObject;
            _FruitCannonScript3 = _FruitCannon3.GetComponent<FruitCannon>();
            _FruitCannonScript3.Target = _Target;
        }
        else
            Debug.LogError("cannons need to be assinged to script");
    }

    private void Update()
    {
        if (_Katana.Count > 0)
        {
            if (_FruitCannons.active == false)
                _FruitCannons.SetActive(true);
            _ScoreText.text = _Score.ToString();
            switch (_Stage)
            {
                case 0:
                    _ScoreText.gameObject.SetActive(true);
                    _FruitCannon1.SetActive(true);
                    _FruitCannonScript1._FireRate = 1;
                    _Score = 0;
                    _Stage = 1;
                    break;
                case 1:
                    if (_Score >= 20)
                    {
                        _FruitCannon2.SetActive(true);
                        _FruitCannonScript2._FireRate = 1f;
                        _FruitCannonScript2._LastShot = _FruitCannonScript1._LastShot + _FruitCannonScript2._FireRate / 3;
                        _Stage = 2;
                    }
                    break;
                case 2:
                    if (_Score >= 40)
                    {
                        _FruitCannon3.SetActive(true);
                        _FruitCannonScript3._FireRate = 1f;
                        _FruitCannonScript3._LastShot = _FruitCannonScript2._LastShot + _FruitCannonScript3._FireRate / 3;
                        _Stage = 3;
                    }
                    break;
                case 3:
                    if (_Score >= 60)
                    {
                        _FruitCannonScript1._FireRate = 0.5f;
                        _FruitCannonScript2._FireRate = 0.5f;
                        _FruitCannonScript3._FireRate = 0.5f;
                        _Stage = 4;
                    }
                    break;
            } 
            TimeScale();
        }
        else
        {
            _Stage = 0;
            _Score = 0;
            _FruitCannon1.SetActive(false);
            _FruitCannon2.SetActive(false);
            _FruitCannon3.SetActive(false);
            _ScoreText.gameObject.SetActive(false);
        }
    }

    void TimeScale()
    {
        if (_SlowTimeBool > 0)
        {
            _CurrentTimeScale -= Time.unscaledDeltaTime / 4;
            _CurrentTimeScale = Mathf.Clamp(_CurrentTimeScale, _MinTimeScale, 1);
            Time.timeScale = _CurrentTimeScale;
        }
        else
        {
            _CurrentTimeScale += Time.unscaledDeltaTime / 4;
            _CurrentTimeScale = Mathf.Clamp(_CurrentTimeScale, _MinTimeScale, 1);
            Time.timeScale = _CurrentTimeScale;
        }
    }

}
