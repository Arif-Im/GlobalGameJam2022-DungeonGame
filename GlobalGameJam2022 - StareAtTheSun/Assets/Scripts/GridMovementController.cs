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

    AnimationManager animationManager;
    Unit unit;

    public Vector2 dir;

    //public static event Action OnEnemyState;

    // Start is called before the first frame update
    void Start()
    {
        unit = transform.GetComponent<Unit>();
        animationManager = GetComponent<AnimationManager>();
        movePoint.parent = null;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animationManager.ChangeAnimationState(AnimationManager.Animations.LEFTWALK);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animationManager.ChangeAnimationState(AnimationManager.Animations.RIGHTWALK);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            animationManager.ChangeAnimationState(AnimationManager.Animations.DOWNWALK);
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            animationManager.ChangeAnimationState(AnimationManager.Animations.UPWALK);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (!unit.isCurrentTurn) { return; }

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
        //else
        //{
        //    if(Vector3.Distance(transform.position, movePoint.position) <= .5f)
        //    {
        //        battleSystem.ChangeTurn(this.unit);
        //    }
        //}
    }
}
