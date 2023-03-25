using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // if (playButtonSmall != null) {
        //     playButtonSmall.SetActive(false);
        // }
        instructionText = GameObject.Find("Instructions");
        //instructionText.SetActive(false);
    }
    
    void Start() {
        // find main menu assets
        startBackground = GameObject.Find("Start Background");
        mainMenuText = GameObject.Find("Main Menu Text");
        startGameButton = GameObject.Find("Start Button");
    }

    /*
        BELOW IS CALLED BY BUTTONS
    */

    // static public void START_GAME_FROM_MAIN_MENU() {
    //     playButtonSmall.SetActive(true);
    //     instructionText.SetActive(true);
    //     startBackground.SetActive(false);
    //     mainMenuText.SetActive(false);
    //     startGameButton.SetActive(false);
    // }

    /*
        PUBLIC METHODS SCENE VERSION
    */

    static public void PLAY_GAME_PRESSED() {
        playButtonSmall.SetActive(false);
    }

    static public void GO_TO_MAIN_MENU() {
        SceneManager.LoadScene("Main Menu");
    }

    static public void GO_TO_LEVEL2() {
        SceneManager.LoadScene("Level Two");
    }

    static public void GO_TO_LEVEL1() {
        SceneManager.LoadScene("Level One");
    }
}
