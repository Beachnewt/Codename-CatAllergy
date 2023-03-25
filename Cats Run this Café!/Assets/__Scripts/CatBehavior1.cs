using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

/*
    NOTES:
    - Behavior A* (kind of): finds shortest unblocked path to gaol
    - Local copy of blocked array for backtracking (not implemeted yet)
        -- List of previous X and Y for the same reason
    - Set catSpeed in MoveCat
    - Implementation includes A LOT of intercommunication with CatCafe
*/

public class CatBehavior1 : MonoBehaviour
{
    static private CatBehavior1 CB1;
    public List<int> prevXPos = new List<int>();
    public List<int> prevYPos = new List<int>();
    // static private Transform[,] tAcB1; // tile transforms array
    private int goalX; 
    private int goalY; 
    private int checkLoops;
    private MoveCat catMover;

    // static allows for no singletons needed for the following variables
    [Header("Dynamic")]
    // Note: view board from bird's eye view with X west and Z south
    private int posX; // X position in array
    private int posY; // Y position in array
    // instantiate called by CatCafe 
    private tileStatus[,] blockedCB1; // tile status for cat behaviour 1
    

    void Awake() {
        catMover = GetComponent<MoveCat>();
        if (this != null) 
        { 
            CB1 = this; 
            posX = 6;
            posY = 9;
            checkLoops = 0;
        }
    }

    void Start() {
        //catMover.enabled = false;
        prevXPos.Add(posX);
        prevYPos.Add(posY);
    }

    static public void BLOCK_TILE_CB1(int x, int y) {
        CB1.blockedCB1[x,y] = tileStatus.blocked;
    }

    static public void INSTANTIATE_BLOCKED_CB1(int len) {
        CB1.blockedCB1 = new tileStatus[len,len];
    }

    static public void INSTANTIATE_GOAL_START(int x, int y) {
        CB1.goalX = x;
        CB1.goalY = y;
    }

    static public void MOVE_CB1_TO_GOAL() {
        CB1.checkLoops++;
        if (CB1.checkLoops > 20) {
            SceneManager.LoadScene("LoseCB1");
        }
        // Go to next level if goal is reached
        if ( (CB1.posX == CB1.goalX) && (CB1.posY == CB1.goalY) ) {
            CB1.catMover.enabled = false;
            CatCafe.START_LEVEL_2();
        }
        int tempX;
        int tempY;
        int currentMin = 101; // Should never be larger than 10 but this is initial case
        int deltaGoal;
        Stack<int> minX = new Stack<int>();
        Stack<int> minY = new Stack<int>();
        // Find the next tile closest to goal and pushes coords to stacks
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                tempX = CB1.posX + i;
                tempY = CB1.posY + j;
                // Check for over array bound (not negative)
                if ( (tempX < 0) || (tempY < 0) ) {
                    continue;
                }
                // Check for under array bound
                if ( (tempX >= CB1.blockedCB1.GetLength(0) ) || 
                        (tempY >= CB1.blockedCB1.GetLength(1) ) ) {
                    continue;
                }
                deltaGoal = findDistanceToGoal(tempX, tempY);
                // Check distance to goal if not blocked
                if ( (CB1.blockedCB1[tempX,tempY] != tileStatus.blocked) &&
                    (currentMin > deltaGoal) ) {
                        minX.Push(tempX);
                        minY.Push(tempY);
                        currentMin = deltaGoal;
                }
            }
        }
        // Update local values and lists
        if ( (minX.Count != 0 || minY.Count != 0) ) {
            tempX = minX.Pop(); 
            tempY = minY.Pop();
            CB1.posX = tempX; 
            CB1.posY = tempY;
            CB1.prevXPos.Add(tempX);
            CB1.prevYPos.Add(tempY);
            CatCafe.GET_REAL_POS_CB1(tempX, tempY);
        }
    }// end findClosestToGoal

    static private int findDistanceToGoal(int a, int b) {
        a -= CB1.goalX;
        if (a < 0) {a = a*-1;} // get abs value
        b -= CB1.goalY;
        if (b < 0) {b = b*-1;} // get abs value
        int total = a + b;
        return total;
    }

    static public void MOVE_CB1_TO_START() {
        MoveCat.MC1.startingMove = true;
        CatCafe.GET_REAL_POS_CB1(CB1.posX, CB1.posY);
    }

    static public void MOVE_CAT(Vector3 targetPosition) {
        MoveCat.UPDATE_TARGET_POSITION(targetPosition);
        CB1.catMover.done = false;
        CB1.catMover.enabled = true;

        // if goal reached and catMover is done moving
        if ( ((CB1.posX == CB1.goalX) && (CB1.posY == CB1.goalY)) &&
               CB1.catMover.done == true ) {
            CB1.catMover.enabled = false;
            CatCafe.START_LEVEL_2();
        }
    } // end MOVE_CAT

}
