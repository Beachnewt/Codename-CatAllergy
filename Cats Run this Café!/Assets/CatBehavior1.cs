using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatBehavior1 : MonoBehaviour
{
    static private CatBehavior1 CB1;
    
    [Header("Dynamic")]
    static private int posX;
    static private int posY;

    void Awake() {
        posX = 6;
        posY = 9;
    }
    
    static public void MOVE_CAT() {
        
    }
}
