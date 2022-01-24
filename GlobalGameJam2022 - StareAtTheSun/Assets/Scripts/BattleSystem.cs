using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { PLAYERTURN, ENEMYTURN }

public class BattleSystem : StateMachine
{
    public BattleState state;

    private void Awake()
    {
        
    }

    private void Start()
    {
        state = BattleState.PLAYERTURN;
    }
}
