using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BattleSystem battleSystem;
    public float attackDamage;

    LayerMask whatIsEnemies;
    public float surroundRange = 1;
    public float forwardRange = 3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Attack");
            battleSystem.state = BattleState.ENEMYTURN;
        }
    }

    private void SurroundAttack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 3), whatIsEnemies);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            Debug.Log("Damage");
            enemy.GetComponent<Unit>().TakeDamage(attackDamage);
        }
    }

    private void ForwardAttack()
    {
        Physics2D.OverlapCircleAll(transform.position, 1);
    }
    private void RangeAttack()
    {
        Physics2D.OverlapCircleAll(transform.position, 1);
    }

    private void AreaAttack()
    {
        Physics2D.OverlapCircleAll(transform.position, 1);
    }
    
}
