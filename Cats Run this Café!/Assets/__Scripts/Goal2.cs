using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal2 : MonoBehaviour
{
    static private Goal2 G;
    private float startPosZ;
    private float startPosX;

    [Header("Inscribed")]
    // choose starting coords in Unity Editor
    public int startCoordX;
    public int startCoordY;

    void Awake() {
        G = this;
    }

    void Start() {
        CatBehavior2.INSTANTIATE_GOAL_START_ROOK(startCoordX, startCoordY);
        startPosZ = transform.position.x;
        startPosX = transform.position.z;
    }
}
