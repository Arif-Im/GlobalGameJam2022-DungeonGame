using System.Collections;
using UnityEngine;

public class EnemyTurn : State
{
    public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        if (!playOnce)
        {
            Debug.Log("Enemy Turn");
            playOnce = true;
        }


        yield break;
        //yield return new WaitForSeconds(0);
        //BattleSystem.SetState(new PlayerTurn(BattleSystem));
    }

    public override IEnumerator Move()
    {
        yield return new WaitForSeconds(0);

        BattleSystem.SetState(new PlayerTurn(BattleSystem));
    }

    public override IEnumerator Attack()
    {
        Debug.Log("Begin Attack");
        yield return new WaitForSeconds(1);
        BattleSystem.SetState(new EnemyTurn(BattleSystem));
    }
}