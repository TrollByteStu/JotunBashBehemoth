using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float secondsLeft = 360f;
    public float timeLimit = 360f;

    private static GameController _instance;
    public GameObject player;
    public GameObject BoatRig;

    // Gannets
    public Transform _GannetIdlePoints;
    public GannetHandler _GannetHandler;

    public List<Bait> activeBait;


    public GameControllerResources gcResources;
    public GameControllerWeather gcWeather;
    public GameControllerInventory gcInventory;
    public GameControllerPointsAndDeaths gcPointsAndDeaths;
    public GameControllerNarrator gcNarrator;
    public GameControllerFruitNinja gcFruitNinja;

    public Bait checkForBaits(string name, Transform ownTransform)
    {
        if (activeBait.Count > 0)
        {
            List<Bait> workingBaits = new List<Bait>();
            foreach( Bait testBait in activeBait)
            {
                if (testBait.doesThisBaitWorkOnMe(name)) 
                    workingBaits.Add(testBait);
            }
            //Debug.Log(workingBaits.Count);
            if ( workingBaits.Count > 0)
            {
                workingBaits.Sort((bait1, bait2) =>
                {
                    float distance1 = (bait1.transform.position - ownTransform.position).sqrMagnitude;
                    float distance2 = (bait2.transform.position - ownTransform.position).sqrMagnitude;
                    return distance1.CompareTo(distance2);
                });
                return workingBaits[0];
            } 
        }
        return null;
    }

    private void Awake()
    {
        timeLimit = secondsLeft;
        _instance = this;
        gcResources = GetComponent<GameControllerResources>();
        gcWeather = GetComponent<GameControllerWeather>();
        gcInventory = GetComponent<GameControllerInventory>();
        gcPointsAndDeaths = GetComponent<GameControllerPointsAndDeaths>();
        gcNarrator = GetComponent<GameControllerNarrator>();
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

    private void Update()
    {
        secondsLeft -= Time.deltaTime;
    }

}
