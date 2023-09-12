using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class watch : MonoBehaviour
{
    public TMP_Text watchText;
    public Material watchGUI;
    GameController GameController;

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        var timeInSecondsInt = (int)GameController.secondsLeft;
        var minutes = timeInSecondsInt / 60;  //Get total minutes
        var seconds = timeInSecondsInt - (minutes * 60);  //Get seconds for display alongside minutes
        watchText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        watchGUI.SetFloat("Vector1_4", ((GameController.secondsLeft / GameController.timeLimit) - 0.5f));
    }
}
