using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    static private Transform ObjOfInterest;
    static private Tile currentTile;
    static private GameObject mainCam;

    void Awake() {
        currentTile = this;
        mainCam = GameObject.Find("Main Camera");
    }

    void OnMouseEnter() {
        // Get status of whether play button has been pressed
        bool playingTile = mainCam.GetComponent<CatCafe>().playing;
        // check if play button has been pressed
        if (playingTile) {
            return;
        }
        if (ObjOfInterest == null) {
            return;
        }
        currentTile = this;
        ObjOfInterest.position = new Vector3(
            currentTile.transform.position.x, 
            0, 
            currentTile.transform.position.z);
    }

    static public void SET_OBJ_OF_INTEREST(Transform obj) {
        ObjOfInterest = obj;
    }

    static public void PLACE_OBJECT() {
        bool playingTile = mainCam.GetComponent<CatCafe>().playing;
        // check if play button has been pressed
        if (playingTile) {
            return;
        }
        if (ObjOfInterest == null) {
            return;
        }
        ObjOfInterest.position = new Vector3(
            currentTile.transform.position.x, 
            0, 
            currentTile.transform.position.z);
        blockPlaced(); // see below
        ObjOfInterest = null;
    }

    static private void blockPlaced() {
        int placedIndex = currentTile.transform.GetSiblingIndex();
        int row = placedIndex / 10;
        int column = placedIndex % 10;
        CatCafe.BLOCK_TILE(row, column);
        MovablePiece.SET_POS(row, column);
    }
}
