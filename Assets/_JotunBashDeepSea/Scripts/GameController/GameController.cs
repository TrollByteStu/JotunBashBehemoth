using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public GameControllerResources gcResources;
    public GameControllerWeather gcWeather;
    public GameObject player;
    public Transform _GannetIdlePoints;


    public List<Bait> activeBait;

    public Bait checkForBaits(GameObject ownPrefab, Transform ownTransform)
    {
        if (activeBait.Count > 0)
        {
            List<Bait> workingBaits = new List<Bait>();
            foreach( Bait testBait in activeBait)
            {
                if (testBait.doesThisBaitWorkOnMe(ownPrefab)) workingBaits.Add(testBait);
            }
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
