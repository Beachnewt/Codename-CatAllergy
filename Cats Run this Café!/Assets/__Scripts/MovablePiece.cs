using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePiece : MonoBehaviour
{
    static private MovablePiece MP;
    static private Transform obstacle;

    [Header("Inscribed")]
    public int posX;
    public int posY;

    void Awake()
    {
        MP = this;
        obstacle = transform;
    }

    void Start() {
        Debug.Log("Blocking" + posX +',' + posY);
        CatCafe.BLOCK_TILE(MP.posX, MP.posY);
    }

    void OnMouseDown() {
        obstacle = this.transform;
        Tile.SET_OBJ_OF_INTEREST(obstacle);
    }

    void OnMouseUp() {
        blockLifted();
        Tile.PLACE_OBJECT();
    }

    static private void blockLifted() {
        CatCafe.UNBLOCK_TILE(MP.posX, MP.posY);
    }

    static public void SET_POS(int x, int y) {
        MP.posX = x;
        MP.posY = y;
    }

}
