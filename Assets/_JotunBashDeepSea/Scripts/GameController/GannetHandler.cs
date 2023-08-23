using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GannetHandler : MonoBehaviour
{
    public int _StartSpawn;
    public int _MaxGannets;
    public float _LastSpawnTime;
    public float _SpawnDelay;
    public List<GameObject> _Gannets;
    public GameObject _GannetPrefab;
    public GameObject _FeatherExplosion;

    // gannet sounds
    public List<AudioClip> _RandomSounds;
    public List<AudioClip> _PainSounds;

    Vector3 RandomSpawnVector(float radius)
    {
        float angle = Random.Range(0f, 360f);

        return new Vector3(Mathf.Sin(angle)* radius, 7 , Mathf.Cos(angle)* radius);
    }

    public void KillGannet(GameObject go)
    {
        var Explosion = Instantiate(_FeatherExplosion, go.transform);
        Destroy(Explosion, 2);
        _Gannets.Remove(go);
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
