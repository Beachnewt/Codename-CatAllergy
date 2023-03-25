using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum tileStatus {
    open, // = on ground floor and not blocked
    blocked, // any level and blocked
    layer2, // on 2nd level
    layer3 // on 3rd level
}

public class CatCafe : MonoBehaviour
{
    static public CatCafe CC; // private Singleton
    static private Transform cT;
    static private string level;

    [Header("Inscribed")]
    public GameObject floorArray; // the array of tiles
    public GameObject rookTheCat;

    [Header("Dynamic")]
    public tileStatus[,] blockedArray; // blocked status array
    public int numChildren;
    public Transform[,] tileArray; // stores transforms
    public bool playing;
    public bool HitCB1Goal; // whether CB1 has reached its goal
    public bool HitRookGoal; // whether Rook has reached its goal

    void Awake()
    {
        CC = this; // Declare Singleton
        cT = CC.floorArray.transform; // gets floorArray (parent of all tiles)
        numChildren = getArraySize(cT.childCount); // gets length/ wideth of square array of tiles
        tileArray = new Transform[numChildren,numChildren]; // instantiate array of tile transforms
        blockedArray = new tileStatus[numChildren,numChildren]; // instantaite array of tiles' blocked status
        playing = false; 
        rookTheCat = GameObject.Find("Rook Cat");
        HitCB1Goal = false; 
        HitRookGoal = false;
        level = SceneManager.GetActiveScene().name; // get current level
    }

    void Start()
    {         
        // Populates tileArray with Transforms of all tiles
        if (CC.numChildren > 0) { // makes sure children exist
            for (int i = 0; i < CC.numChildren; i++) {
                for (int j = 0; j < CC.numChildren; j++) {
                    CC.tileArray[i,j] = cT.GetChild(j + (10*i));
                }
            }
        }
        if (level == "Level Two") 
        {
            CC.rookTheCat.SetActive(true); // NO ROOK FOR LEVEL ONE (default Rook to not active)
            CatBehavior2.SET_ROOK_BLOCKED_LEN(numChildren);
            return;
        }
        if (level == "Level One") {
            // instantiates CB1's blocked array with the same length
            CatBehavior1.INSTANTIATE_BLOCKED_CB1(numChildren);
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

    static private void moveCatsToStart() {
        if (level == "Level Two") 
        {
            CatBehavior2.MOVE_ROOK_TO_START();
            return;
        }
        CatBehavior1.MOVE_CB1_TO_START();
    }

    static public void BLOCK_TILE(int xPos, int yPos) {
        CC.blockedArray[xPos, yPos] = tileStatus.blocked;
    }

    static public void UNBLOCK_TILE(int xPos, int yPos) {
        CC.blockedArray[xPos, yPos] = tileStatus.open;
    }

    static public void PLAY_BUTTON_PRESS() {
        CC.playing = true;
        if (level == "Level Two")
        {
            CatBehavior2.MOVE_ROOK_TO_GOAL();
            return;
        }
        // Copy blocked tiles to cats' local
        for ( int x = 0; x < CC.blockedArray.GetLength(0); x++ ) {
            for ( int y = 0; y < CC.blockedArray.GetLength(1); y++ ) {
                if ( CC.blockedArray[x, y] == tileStatus.blocked ) {
                    CatBehavior1.BLOCK_TILE_CB1(x, y);
                }
            }
        }
        CatBehavior1.MOVE_CB1_TO_GOAL();
    }

    static public void SET_STATUS_ARRAY_VAL(int xPos, int yPos) {
        CC.blockedArray[xPos,yPos] = tileStatus.blocked;
    }
    
    // Translates coordinates from 2D array to
    // real coordiantes in the 3D space and
    // sends them back to CatBehavior1
    static public void GET_REAL_POS_CB1 (int xCoord, int yCoord) {
        Vector3 targetPosition = CC.tileArray[xCoord, yCoord].position;
        CatBehavior1.MOVE_CAT(targetPosition);
    }

    // static public void CHECK_BLOCKED() {
    //     Tile.SET_TILE_BLOCKED_STATUS()
    // }

    //  BELOW IS CODE FOR LEVEL 2 (may use code from level 1)
    /*   
        Notes:
        - Introduce Rook (second cat)
        - Bring back play button
    */

    static public void START_LEVEL_2() {
        // Introduce new cat 
        // Turn back on play button
        // GUIManager.START_LEVEL_2();
        // TODO: Load next scene
        SceneManager.LoadScene("Level Two");
    }

    static public void IS_BLOCKED(int x, int y) {
        CatBehavior2.SET_ROOK_TS(CC.blockedArray[x, y]);
    }

    static public void GET_REAL_POS_ROOK(int x, int y) {
        //Debug.Log("GET REAL POS ROOK call back");
        //Debug.Log("GET REAL POS ROOK call back: " + CC.tileArray[x, y].position);
        CatBehavior2.MOVE_ROOK(CC.tileArray[x, y].position);
    }

}
