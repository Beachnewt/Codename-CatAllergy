using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    static private tileStatus ts;
    private MoveRook rookMover; // TODO: MAKE CLASS
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
        ts = status;
    }

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
        if ( (posX == goalX) && (posY == goalY) ) {
            return;
        }
        CatCafe.IS_BLOCKED(posX, posY);
        while ((posX != goalX) && (posY != goalY))  {
            Debug.Log(turns);
            switch (turns) {
                case 0:
                // if turns = 0, go straight from start
                    while(ts == tileStatus.open) {
                        posY--;
                        // Check for over array bound (not negative)
                        if (posY < 0) {
                            break;
                        }
                        CatCafe.IS_BLOCKED(posX, posY);
                    }
                    posY++;
                    turns++;
                    ts = tileStatus.open;
                    CatCafe.GET_REAL_POS_ROOK(posX, posY);
                    break;
                case 1:
                    while(ts == tileStatus.open) {
                        posX--;
                        // Check for over array bound (not negative)
                        if (posX < 0) {
                            break;
                        }                        
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
                        CatCafe.IS_BLOCKED(posX, posY);
                    }
                    posX++;
                    turns++;
                    ts = tileStatus.open;
                    CatCafe.GET_REAL_POS_ROOK(posX, posY);
                    break;
                default:
                // TODO: Notify CatCafe Rook finished for end game
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
