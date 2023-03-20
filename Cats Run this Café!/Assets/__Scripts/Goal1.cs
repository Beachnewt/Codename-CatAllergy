using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal1 : MonoBehaviour
{
    static private Goal1 G;
    private float startPosZ;
    private float startPosX;

    [Header("Inscribed")]
    // choose starting coords in Unity
    public int startCoordX;
    public int startCoordY;

    void Awake() {
        G = this;
    }

    void Start() {
        CatBehavior1.INSTANTIATE_GOAL_START(startCoordX, startCoordY);
        startPosZ = transform.position.x;
        startPosX = transform.position.z;
    }

    // static public void CHANGE_GOAL1_POS(int x, int y) {
        
    // }
}
