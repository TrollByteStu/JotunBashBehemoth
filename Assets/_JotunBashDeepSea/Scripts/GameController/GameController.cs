using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public GameControllerResources gcResources;
    public GameControllerWeather gcWeather;
    public GameObject player;

    public List<Bait> activeBait;

    private void Awake()
    {
        _instance = this;
    }

    public static GameController Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game manager is NULL");
            return _instance;
        }
    }

}
