using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BattleSystem battleSystem;
    public float attackDamage;

    public LayerMask whatIsEnemies;
    public float surroundRange = 1;
    public float closeRange = 1;
    public float longRange = 3;

    List<Enemy> listOfEnemies = new List<Enemy>();

    Vector2 dir;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Attack");
            battleSystem.state = BattleState.ENEMYTURN;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            dir = Vector2.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            dir = Vector2.right;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            dir = Vector2.down;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            dir = Vector2.up;
        }
    }

    public void RoundAttack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, surroundRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            Debug.Log(enemiesToDamage[i].name);
            enemiesToDamage[i].gameObject.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void ForwardAttack()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, dir, closeRange, whatIsEnemies);

        if (hitEnemy.collider != null)
        {
            hitEnemy.transform.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    public void RangeAttack()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, dir, longRange, whatIsEnemies);

        if (hitEnemy.collider != null)
        {
            hitEnemy.transform.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void AreaAttack()
    {
        foreach(Enemy enemy in listOfEnemies)
        {
            enemy.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, surroundRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + dir);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + (dir * longRange));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Room>())
        {
            listOfEnemies = collision.gameObject.GetComponent<Room>().GetListOfEnemies();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        listOfEnemies.Clear();
    }
}
