using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubberDucky : MonoBehaviour
{
    public AudioSource BombTicking;
    public AudioSource Squeaking;
    public int squeakDelay = 360;

    public bool stillTicking = true;

    public GameObject explosionPrefab;

    public bool isDud = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( !BombTicking.isPlaying && stillTicking)
        {
            if ( isDud )
            {
                stillTicking = false;
                Squeaking.Play();
            } else
            { // not a dud
                var explotion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explotion, 5);
                Destroy(gameObject);
            }
            
        }

        if ( !stillTicking && !Squeaking.isPlaying)
        {
            if ( Random.Range(1, squeakDelay) == 1)
            {
                Squeaking.Play();
            }
        }
    }
}
