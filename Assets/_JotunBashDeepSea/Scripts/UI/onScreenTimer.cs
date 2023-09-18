using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class onScreenTimer : MonoBehaviour
{

    private TMP_Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var timeInSecondsInt = (int)GameController.Instance.secondsLeft;
        var minutes = timeInSecondsInt / 60;  //Get total minutes
        var seconds = timeInSecondsInt - (minutes * 60);  //Get seconds for display alongside minutes
        myText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}
