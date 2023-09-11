using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDinner : MonoBehaviour
{
    private float timeLeft;
    private CanvasGroup myCG;

    // Start is called before the first frame update
    void Awake()
    {
        myCG = GetComponent<CanvasGroup>();
        timeLeft = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0f) timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f && myCG.alpha > 0f) myCG.alpha -= Time.deltaTime;
        if (timeLeft <= 0f && myCG.alpha <= 0f) gameObject.SetActive(false);
    }
}
