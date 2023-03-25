using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameButton : MonoBehaviour
{
    public GameObject playButtonSmall;
    static private PlayGameButton PGB;
    void Awake(){
        PGB = this;
        PGB.playButtonSmall = GameObject.Find("Play Button");
        PGB.playButtonSmall.SetActive(false);
    }

    static public void HIDE_MAIN_SCREEN() {
        GameObject startBackground = GameObject.Find("Start Background");
        GameObject startText = GameObject.Find("Main Menu Text");
        GameObject startGameButton = GameObject.Find("Start Button");
        startBackground.SetActive(false);
        startText.SetActive(false);
        startGameButton.SetActive(false);
        PGB.playButtonSmall.SetActive(true);
    }
}
