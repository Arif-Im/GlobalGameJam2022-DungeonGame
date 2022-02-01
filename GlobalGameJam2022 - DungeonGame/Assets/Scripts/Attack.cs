using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDamage;

    public LayerMask whatIsEnemies;
    public float surroundRange = 1;
    public float closeRange = 1;
    public float longRange = 3;

    BattleSystem battleSystem;
    Unit unit;

    List<Unit> listOfEnemies = new List<Unit>();

    Vector2 dir;

    private void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Attack");
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
        if(!unit.isCurrentTurn) { return; }

        GameObject blast = transform.Find("Blast").gameObject;
        ParticleSystem blastPS = blast.GetComponent<ParticleSystem>();
        blastPS.Play();

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, surroundRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            Debug.Log(enemiesToDamage[i].name);
            enemiesToDamage[i].gameObject.GetComponent<Unit>().TakeDamage(attackDamage);
        }
        battleSystem.ChangeTurn(unit);
    }

    public void ForwardAttack()
    {
        if (!unit.isCurrentTurn) { return; }
        GameObject bash = transform.Find("Bash").gameObject;
        ForwardAttackAnim(bash);

        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, dir, closeRange, whatIsEnemies);

        if (hitEnemy.collider != null)
        {
            hitEnemy.transform.GetComponent<Unit>().TakeDamage(attackDamage);
        }
        battleSystem.ChangeTurn(unit);
    }

    public void RangeAttack()
    {
        if (!unit.isCurrentTurn) { return; }
        GameObject flamethrower = transform.Find("Flamethrower").gameObject;
        ForwardAttackAnim(flamethrower);

        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, dir, longRange, whatIsEnemies);

        if (hitEnemy.collider != null)
        {
            hitEnemy.transform.GetComponent<Unit>().TakeDamage(attackDamage);
        }
        battleSystem.ChangeTurn(unit);
    }

    public void AreaAttack()
    {
        if (!unit.isCurrentTurn) { return; }
        GameObject blizzard = transform.Find("Blizzard").gameObject;
        ParticleSystem blizzardPS = blizzard.GetComponent<ParticleSystem>();
        blizzardPS.Play();

        foreach (Enemy enemy in listOfEnemies)
        {
            enemy.TakeDamage(attackDamage);
        }
        battleSystem.ChangeTurn(unit);
    }
    private void ForwardAttackAnim(GameObject pFXGO)
    {
        GameObject bash = pFXGO;
        ParticleSystem bashPS = bash.GetComponent<ParticleSystem>();

        if (dir == Vector2.left)
        {
            bash.transform.rotation = Quaternion.Euler(0, -90, 0);
            bashPS.Play();
        }
        else if (dir == Vector2.right)
        {
            bash.transform.rotation = Quaternion.Euler(180, -90, 0);
            bashPS.Play();
        }
        else if (dir == Vector2.down)
        {
            bash.transform.rotation = Quaternion.Euler(90, -90, 0);
            bashPS.Play();
        }
        else if (dir == Vector2.up)
        {
            bash.transform.rotation = Quaternion.Euler(-90, -90, 0);
            bashPS.Play();
        }
        battleSystem.ChangeTurn(unit);
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

    private void OnTriggerStay2D(Collider2D collision)
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
