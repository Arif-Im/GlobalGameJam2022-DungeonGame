using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopMovement;
    public BattleSystem battleSystem;
    Unit unit;

    public Vector2 dir;

    //public static event Action OnEnemyState;

    // Start is called before the first frame update
    void Start()
    {
        unit = transform.GetComponent<Unit>();
        movePoint.parent = null;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!unit.isCurrentTurn) { return; }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .5f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                    battleSystem.ChangeTurn(this.unit);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                    battleSystem.ChangeTurn(this.unit);
                }
            }
        }
    }
}
