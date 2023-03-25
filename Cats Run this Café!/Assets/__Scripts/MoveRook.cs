using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveRook : MonoBehaviour
{
    static public MoveRook MR;

    [SerializeField]
    private bool _done = false;
    private bool _lMove = false;

    public bool done {
        get { return _done; }
        set { _done = value;}
    }

    public bool lMove {
        get { return _lMove; }
        set { _lMove = value;}
    }

    [Header("Inscribed")]
    public float catSpeed;

    [Header("Dynamic")]
    private Vector3 targetPosition; // starting position

    void Awake() {
        MR = this;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(
                transform.position, 
                targetPosition, 
                catSpeed * Time.deltaTime);
        if (transform.position == targetPosition) {
            done = true;
            if (!lMove) 
            { 
                //Debug.Log("Rook Mover Call Back");
                CatBehavior2.MOVE_ROOK_TO_GOAL(); 
            }
            //Debug.Log("MoveRook: reached goal");
            if (lMove && transform.position == targetPosition) { SceneManager.LoadScene("End Screen"); }
        }
    }

    static public void UPDATE_TARGET_POSITION(Vector3 finalPos) {
        MR.targetPosition = finalPos;
    }

    static public void TOGGLE_LAST_MOVE() {
        MR.lMove = !MR.lMove;
    }
}
