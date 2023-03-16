using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum tileStatus {
    // null = on ground floor and not blocked
    blocked, // any level and blocked
    layer2, // on 2nd level
    layer3 // on 3rd level
}

public class CatCafe : MonoBehaviour
{
    static private CatCafe CC; // private Singleton
    static private Transform cT;

    [Header("Inscribed")]
    public GameObject floorArray; // the array of tiles
    public GameObject heldItem; // obj being moved

    [Header("Dynamic")]
    public tileStatus[,] blockedArray; // blocked status array
    public int numChildren;
    public Transform[,] tileArray;

    void Awake()
    {
        CC = this;
        cT = CC.floorArray.transform;
        numChildren = getArraySize(cT.childCount);
        tileArray = new Transform[numChildren,numChildren];
        CatBehavior1.INSTANTIATE_BLOCKED_CB1(numChildren);
    }

    void Start()
    {
        // Get Transforms of all tiles
        if (CC.numChildren > 0) {
            for (int i = 0; i < CC.numChildren; i++) {
                for (int j = 0; j < CC.numChildren; j++) {
                    CC.tileArray[i,j] = cT.GetChild(j + (10*i));
                }
            }
        }
    }

    static public void PLAY_BUTTON_PRESS() {
        // Check which tiles are blocked
        // TODO: change from transform to block check
        // if (CC.numChildren > 0) {
        //     for (int i = 0; i < CC.numChildren; i++) {
        //         for (int j = 0; j < CC.numChildren; j++) {
        //             CC.tileArray[i,j] = cT.GetChild(j + (10*i));
        //         }
        //     }
        // }
        // Copy blocked tiles to cats' local
        for ( int x = 0; x < CC.blockedArray.GetLength(0); x++ ) {
            for ( int y = 0; y < CC.blockedArray.GetLength(1); y++ ) {
                CatBehavior1.BLOCK_TILE_CB1(x, y);
            }
        }
    }

    // gets true size of a SQUARE array
    static private int getArraySize(int children) {
        for (int i = 10; i > 0; i--) {
            if ((children/ i) == i) {
                return i;
            }
        }
        return 0;
    }

    static public void SET_STATUS_ARRAY_VAL(int xPos, int yPos) {
        CC.blockedArray[xPos,yPos] = tileStatus.blocked;
    }
    
    // Translates coordinates from 2D array to
    // real coordiantes in the 3D space and
    // sends them back to CatBeahvior1
    static public void GET_REAL_POS_CB1 (int xCoord, int yCoord) {
        float zPos = CC.tileArray[xCoord, yCoord].position.z;
        float xPos = CC.tileArray[xCoord, yCoord].position.x;
        CatBehavior1.MOVE_CAT(zPos, xPos);
    }

}
