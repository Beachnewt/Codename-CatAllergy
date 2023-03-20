using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRook : MonoBehaviour
{
    static public MoveRook MR;

    [SerializeField]
    private bool _done = false;

    public bool done {
        get { return _done; }
        set { _done = value;}
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
            CatBehavior2.MOVE_ROOK_TO_GOAL();
        }
    }

    static public void UPDATE_TARGET_POSITION(Vector3 finalPos) {
        Debug.Log("Updated MoveRook targetPos");
        Debug.Log(finalPos);
        MR.targetPosition = finalPos;
    }
}
