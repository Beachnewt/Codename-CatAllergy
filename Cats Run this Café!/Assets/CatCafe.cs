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
    public Transform[,] tileArray;
    public tileStatus[,] blockedArray; // blocked status array
    public float[,] distFromGoalX; // up and down dist
    public float[,] distFromGoalZ; // left/right dist
    public int numChildren;

    void Awake()
    {
        CC = this;
        cT = CC.floorArray.transform;
        numChildren = getArraySize(cT.childCount);
        tileArray = new Transform[numChildren,numChildren];
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
        if (CC.numChildren > 0) {
            for (int i = 0; i < CC.numChildren; i++) {
                for (int j = 0; j < CC.numChildren; j++) {
                    CC.tileArray[i,j] = cT.GetChild(j + (10*i));
                }
            }
        }

    }

    // gets true size of SQUARE array
    static private int getArraySize(int children) {
        for (int i = 10; i > 0; i--) {
            if ((children/ i) == i) {
                return i;
            }
        }
        return 0;
    }

}
