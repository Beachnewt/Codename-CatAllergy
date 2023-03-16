using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static private Goal G;
    private float startPosZ;
    private float startPosX;

    [Header("Inscribed")]
    public int startCoordX;
    public int startCoordY;

    void Awake() {
        G = this;
        CatBehavior1.INSTANTIATE_GOAL_START(startCoordX, startCoordY);
    }

    void Start() {
        startPosZ = transform.position.x;
        startPosX = transform.position.z;
    }
}
