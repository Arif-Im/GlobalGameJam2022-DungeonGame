using System.Collections;
using UnityEngine;

public class EnemyTurn : State
{
    public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        Debug.Log("Enemy Turn");
        yield return new WaitForSeconds(1);
        BattleSystem.SetState(new PlayerTurn(BattleSystem));
    }

    public override IEnumerator Move()
    {
        Debug.Log("Begin Move");
        yield return new WaitForSeconds(1);
        BattleSystem.SetState(new EnemyTurn(BattleSystem));
    }

    public override IEnumerator Attack()
    {
        Debug.Log("Begin Attack");
        yield return new WaitForSeconds(1);
        BattleSystem.SetState(new EnemyTurn(BattleSystem));
    }
}