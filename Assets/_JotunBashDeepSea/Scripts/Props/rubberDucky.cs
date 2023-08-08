using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rubberDucky : MonoBehaviour
{
    public AudioSource BombTicking;
    public AudioSource Squeaking;
    public int squeakDelay = 360;

    public bool stillTicking = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( !BombTicking.isPlaying && stillTicking)
        {
            stillTicking = false;
            Squeaking.Play();
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
