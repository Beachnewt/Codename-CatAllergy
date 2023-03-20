using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCat : MonoBehaviour
{
    static public MoveCat MC1;

    [SerializeField]
    private bool _done = false;
    private bool _startingMove = false;

    public bool done {
        get { return _done; }
        set { _done = value;}
    }

    public bool startingMove {
        get { return _startingMove; }
        set { _startingMove = value;}
    }

    [Header("Inscribed")]
    public float catSpeed;

    [Header("Dynamic")]
    private Vector3 targetPosition; // starting position

    void Awake() {
        MC1 = this;
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
            if (!startingMove) {
                startingMove = false;
                CatBehavior1.MOVE_CB1_TO_GOAL();
            }
        }
    }

    static public void UPDATE_TARGET_POSITION(Vector3 finalPos) {
        MC1.targetPosition = finalPos;
    }
}
