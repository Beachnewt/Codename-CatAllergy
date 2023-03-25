using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    NOTES:  
    - Behavior: Move in straight line from start until obstacle
                is hit, then turn right and repeat until goal
                is found or (4-6 [TBD]) turns have been made
    - CatBehavior2/ CB2 has a codename of *Rook* based on its movement pattern
*/

public class CatBehavior2 : MonoBehaviour
{
    static private CatBehavior2 Rook;
    static private int goalX;
    static private int goalY;
    static private tileStatus ts; // status of tile being looked at
    private MoveRook rookMover;
    static private int blockedArrayLen;
    
    [Header("Dynamic")]
    static private int posX; // X position in array
    static private int posY; // Y position in array
    static private int turns;
    // Note: blocked tiles will be received from CatCafe

    void Awake() {
        rookMover = GetComponent<MoveRook>();
        Rook = this;
        posX = 7;
        posY = 9;
        turns = 0;
    }

    void Start() {
        rookMover.enabled = false;
    }

    static public void INSTANTIATE_GOAL_START_ROOK(int x, int y) {
        goalX = x;
        goalY = y;
    }

    static public void SET_ROOK_BLOCKED_LEN(int num) {
        blockedArrayLen = num;
    }

    static public void SET_ROOK_TS(tileStatus status) {
        //Debug.Log("Setting Rook ts: " + status);
        ts = status;
    }

    // Called by CatCafe to move cat to start
    static public void MOVE_ROOK_TO_START() {
        // Places blocked status of tile in *ts*, and
        // gets blocked status of starting square
        CatCafe.IS_BLOCKED(posX, posY);
        // Moves Rook to start
        if (ts != tileStatus.blocked) {
            CatCafe.GET_REAL_POS_ROOK(posX, posY);
        }
    }

    static public void MOVE_ROOK_TO_GOAL() {
        // prevents infinite loop
        if (Rook.rookMover.done == true) { Rook.rookMover.enabled = false; }
            //Debug.Log("MOVE_ROOK_TO_GOAL (posX,posY,goalX, goalY): " + posX + ',' + posY + ',' + goalX + ',' + goalY);
        // Check if goal is reached
        if ( (posX == goalX) && (posY == goalY) ) {
            //Debug.Log("Rook reached goal");
            Rook.rookMover.enabled = false;
            SceneManager.LoadScene("End Screen");
        }
        //Debug.Log("MOVE ROOK TO GOAL BLOCK CHECK");
        CatCafe.IS_BLOCKED(posX, posY);
        if ((posX != goalX) || (posY != goalY))  {
            //Debug.Log("Turns: " + turns);
            switch (turns) {
                case 0:
                // if turns = 0, go straight from start
                    while(ts == tileStatus.open) {
                        posY--;
                        // Check for over array bound (not negative)
                        if (posY < 0) {
                            break;
                        }
                        if ( ((posX == goalX) && (posY == goalY)) &&
                            Rook.rookMover.done == true ) {
                            posY--;
                            MoveRook.TOGGLE_LAST_MOVE();
                            break;
                        }
                        //Debug.Log("Case 0: posX: " + posX + "and posY: " + posY);
                        CatCafe.IS_BLOCKED(posX, posY);
                    }
                    posY++;
                    turns++;
                    ts = tileStatus.open;
                    CatCafe.GET_REAL_POS_ROOK(posX, posY);
                    break;
                case 1:
                    //Debug.Log(posX);
                    while(ts == tileStatus.open) {
                        posX--;
                        // Check for over array bound (not negative)
                        if (posX < 0) {
                            break;
                        }
                        //Debug.Log("Case 1 Fix: " + posX + "," + posY);
                        if ( ((posX == goalX) && (posY == goalY)) &&
                            Rook.rookMover.done == true ) {
                                posX--;
                                MoveRook.TOGGLE_LAST_MOVE();
                                break;
                        }
                        //Debug.Log("Case 1: posX: " + posX + "and posY: " + posY);                        
                        CatCafe.IS_BLOCKED(posX, posY);
                    }
                    posX++;
                    turns++;
                    ts = tileStatus.open;
                    CatCafe.GET_REAL_POS_ROOK(posX, posY);
                    break;
                case 2:
                    while(ts == tileStatus.open) {
                        posY++;                        
                        // Check for under array bound
                        if ( posY >= blockedArrayLen) {
                            break;
                        }
                        if ( ((posX == goalX) && (posY == goalY)) &&
                            Rook.rookMover.done == true ) {
                            posY++;
                            MoveRook.TOGGLE_LAST_MOVE();
                            break;
                        }
                        //Debug.Log("Case 2: posX: " + posX + "and posY: " + posY);    
                        CatCafe.IS_BLOCKED(posX, posY);
                    }
                    posY--;
                    turns++;
                    ts = tileStatus.open;
                    CatCafe.GET_REAL_POS_ROOK(posX, posY);
                    break;
                case 3:
                    while(ts == tileStatus.open) {
                        posX++;                        
                        // Check for under array bound
                        if ( posX >= blockedArrayLen) {
                            break;
                        }
                        if ( ((posX == goalX) && (posY == goalY)) &&
                            Rook.rookMover.done == true ) {
                            posX++;
                            MoveRook.TOGGLE_LAST_MOVE();
                            break;
                        }
                        //Debug.Log("Case 3: posX: " + posX + "and posY: " + posY);    
                        CatCafe.IS_BLOCKED(posX, posY);
                    }
                    posX--;
                    turns++;
                    ts = tileStatus.open;
                    CatCafe.GET_REAL_POS_ROOK(posX, posY);
                    break;
                case 4:
                    while(ts == tileStatus.open) {
                        posY--;
                        // Check for over array bound (not negative)
                        if (posY < 0) {
                            break;
                        }
                        if ( ((posX == goalX) && (posY == goalY)) &&
                            Rook.rookMover.done == true ) {
                            posY--;
                            MoveRook.TOGGLE_LAST_MOVE();
                            break;
                        }
                        //Debug.Log("Case 4: posX: " + posX + "and posY: " + posY);      
                        CatCafe.IS_BLOCKED(posX, posY);
                    }
                    posY++;
                    turns++;
                    ts = tileStatus.open;
                    CatCafe.GET_REAL_POS_ROOK(posX, posY);
                    break;
                case 5:
                    while(ts == tileStatus.open) {
                        posX--;
                        // Check for over array bound (not negative)
                        if (posX < 0) {
                            break;
                        }
                        //Debug.Log("Case 5: " + posX);
                        if ( ((posX == goalX) && (posY == goalY)) &&
                            (Rook.rookMover.done == true) ) {
                            posX--;
                            MoveRook.TOGGLE_LAST_MOVE();
                            break;
                        }                           
                        CatCafe.IS_BLOCKED(posX, posY);
                    }
                    posX++;
                    turns++;
                    ts = tileStatus.open;
                    CatCafe.GET_REAL_POS_ROOK(posX, posY);
                    break;
                default:
                // TODO: Notify CatCafe Rook failed to reach goal
                    Rook.rookMover.enabled = false;
                    //Debug.Log("default case");
                    SceneManager.LoadScene("LoseRook");
                    break;
            }
        } // end while loop
    }

    static public void MOVE_ROOK(Vector3 targetPosition) {
        MoveRook.UPDATE_TARGET_POSITION(targetPosition);
        Rook.rookMover.done = false;
        Rook.rookMover.enabled = true;

         if ( ((posX == goalX) && (posY == goalY)) &&
               Rook.rookMover.done == true ) {
            Rook.rookMover.enabled = false;
        }
    }

}
