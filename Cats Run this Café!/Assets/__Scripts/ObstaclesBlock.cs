using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesBlock : MonoBehaviour
{
    [Header("Inscribed")]
    //public int[,] obstacleCoords;
    public int arraySize;

    // brute force blocking
    void Start() {
        //obstacleCoords = new int[arraySize,2];
        for (int x = 0; x < arraySize; x++) {
            CatCafe.BLOCK_TILE(4, x);
        }
    }
}
