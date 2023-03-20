using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePiece : MonoBehaviour
{
    static private MovablePiece MP;
    static private Transform obstacle;

    [Header("Inscribed")]
    static public int posX;
    static public int posY;

    void Awake()
    {
        MP = this;
        obstacle = transform;
    }

    void Start() {
        CatCafe.BLOCK_TILE(posX, posY);
    }

    void OnMouseDown() {
        Tile.SET_OBJ_OF_INTEREST(obstacle);
    }

    void OnMouseUp() {
        blockLifted();
        Tile.PLACE_OBJECT();
    }

    static private void blockLifted() {
        CatCafe.UNBLOCK_TILE(posX, posY);
    }

    static public void SET_POS(int x, int y) {
        posX = x;
        posY = y;
    }

}
