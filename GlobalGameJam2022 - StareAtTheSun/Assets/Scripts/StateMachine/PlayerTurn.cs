using System.Collections;
using UnityEngine;

public class PlayerTurn : State
{
    public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        if(!playOnce)
        {
            Debug.Log("Player Turn");
            playOnce = true;
        }
        yield break;
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