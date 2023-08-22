using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GannetHandler : MonoBehaviour
{
    public float _LastSpawnTime;
    public float _SpawnDelay;
    public GameObject _GannetPrefab;
    public int _StartSpawn;
    public int _MaxGannets;
    public List<GameObject> _Gannets;

    Vector3 RandomSpawnVector(float radius)
    {
        float angle = Random.Range(0f, 360f);

        return new Vector3(Mathf.Sin(angle)* radius, 7 , Mathf.Cos(angle)* radius);
    }

    private void Start()
    {
        _Gannets.Clear();
        for (int i = 0; i < _StartSpawn; i++)
        {
            var Gannet = Instantiate(_GannetPrefab, RandomSpawnVector(20), Quaternion.identity);
            _Gannets.Add(Gannet);
        }
    }

    private void FixedUpdate()
    {
        if (_Gannets.Count < _MaxGannets)
        {
            if (_LastSpawnTime + _SpawnDelay < Time.time)
            {
                var Gannet = Instantiate(_GannetPrefab, RandomSpawnVector(20), Quaternion.identity);
                _Gannets.Add(Gannet);
                _LastSpawnTime = Time.time;
            }
        }
        else
            _LastSpawnTime = Time.time;

    }
}
