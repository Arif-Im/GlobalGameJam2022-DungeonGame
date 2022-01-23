using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    public Begin(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        //Debug.Log("Start Game");
        yield return new WaitForSeconds(2);
        BattleSystem.SetState(new PlayerTurn(BattleSystem));
    }
}