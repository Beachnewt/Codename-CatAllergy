using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatBehavior1 : MonoBehaviour
{
    static private CatBehavior1 CB1;
    static List<int> prevXPos = new List<int>();
    static List<int> prevYPos = new List<int>();
    // static private Transform[,] tAcB1; // tile transforms array
    
    // static allows for no singletone needed for the following variables
    [Header("Dynamic")]
    static private int goalX;
    static private int goalY;
    static private int posX;
    static private int posY;
    static private int[,] distFromGoal;
    static private tileStatus[,] blockedCB1; // tile status for cat behaviour 1

    void Awake() {
        posX = 6;
        posY = 9;
    }

    static public void BLOCK_TILE_CB1(int x, int y) {
        blockedCB1[x,y] = tileStatus.blocked;
    }

    static public void INSTANTIATE_BLOCKED_CB1(int len) {
        blockedCB1 = new tileStatus[len,len];
    }

    static public void INSTANTIATE_GOAL_START(int x, int y) {
        goalX = x;
        goalY = y;
    }
    
    static public void MOVE_CAT(float zPos, float xPos) {
        //TODO: MOVE CAT BASED ON POS
    } // end MOVE_CAT

    static private void findClosestToGoal(int x, int y) {
        int tempX;
        int tempY;
        int currentMin = 101; // Should never be larger than 10 but this is initial case
        int deltaGoal;
        Stack<int> minX = new Stack<int>();
        Stack<int> minY = new Stack<int>();
        // Find the next tile closest to goal and pushes coords to stacks
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                tempX = posX + i;
                tempY = posY + j;
                deltaGoal = findDistanceToGoal(tempX, tempY);
                // Check for inbounds
                if ( (tempX > blockedCB1.GetLength(0)) || (tempY > blockedCB1.GetLength(1)) ) {
                    continue;
                }
                // Check distance to goal if not blocked
                if ( (blockedCB1[tempX,tempY] != tileStatus.blocked) &&
                    (currentMin > deltaGoal) ) {
                        minX.Push(tempX);
                        minY.Push(tempY);
                        currentMin = deltaGoal;
                }
            }
        }
        CatCafe.GET_REAL_POS_CB1(minX.Pop(), minY.Pop());
    }// end findClosestToGoal

    static private int findDistanceToGoal(int a, int b) {
        a -= goalX;
        if (a < 0) {a = a*-1;}
        b -= goalY;
        if (b < 0) {b = b*-1;}
        int total = a + b;
        return total;
    }
}
