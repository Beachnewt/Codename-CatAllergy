using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    static private Transform ObjOfInterest;
    static private Tile currentTile;
    static private GameObject mainCam;
    //static private boolean isBlocked;

    void Awake() {
        currentTile = null;
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
        if (currentTile == null) {
            return;
        }
        int placedIndex = currentTile.transform.GetSiblingIndex();
        int row = placedIndex / 10;
        int column = placedIndex % 10;
        if (CatCafe.CC.blockedArray[row, column] == tileStatus.blocked) {
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
        Debug.Log("blockedPlaced; row: " + row + "col: " + column);
        MovablePiece.SET_POS(row, column);
    }

    // static private void SET_TILE_BLOCKED_STATUS(tileStatus value) {
    //     isBlocked = value;
    // }
}
