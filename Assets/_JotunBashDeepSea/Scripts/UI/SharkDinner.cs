using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDinner : MonoBehaviour
{
    private float timeLeft;
    private CanvasGroup myCG;
    private AudioSource myAS;

    void resetUI()
    {
        timeLeft = 5f;
        myCG.alpha = 1f;
        myAS.Stop();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Awake()
    {
        myCG = GetComponent<CanvasGroup>();
        myAS = GetComponent<AudioSource>();
        resetUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            if ( !myAS.isPlaying) myAS.Play();
        }
        if (timeLeft <= 0f && myCG.alpha > 0f) myCG.alpha -= Time.deltaTime;
        if (timeLeft <= 0f && myCG.alpha <= 0f)
        {
            resetUI();
            gameObject.SetActive(false);
        }
    }
}
