using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOTE: PUT IN CANVAS IN HIERARCHY
*/

public class GUIManager : MonoBehaviour
{
    static private GUIManager manager; 
    static private GameObject playButtonSmall; // bottom right corner button
    static private GameObject instructionText; // in-game instructions
    static private GameObject startBackground; // main menu start
    static private GameObject mainMenuText; // main menu title text
    static private GameObject startGameButton; // main menu play game button
        
    void Awake() {
        manager = this;
        // get small play button and deactivate
        playButtonSmall = GameObject.Find("Play Button");
        playButtonSmall.SetActive(false);
        instructionText = GameObject.Find("Instructions");
        instructionText.SetActive(false);
    }
    
    void Start() {
        // find main menu assets
        startBackground = GameObject.Find("Start Background");
        mainMenuText = GameObject.Find("Main Menu Text");
        startGameButton = GameObject.Find("Start Button");
    }

    static public void START_GAME_FROM_MAIN_MENU() {
        playButtonSmall.SetActive(true);
        instructionText.SetActive(true);
        startBackground.SetActive(false);
        mainMenuText.SetActive(false);
        startGameButton.SetActive(false);
    }

    static public void PLAY_GAME_PRESSED() {
        playButtonSmall.SetActive(false);
    }

    static public void START_LEVEL_2() {
        playButtonSmall.SetActive(true);
    }
}
