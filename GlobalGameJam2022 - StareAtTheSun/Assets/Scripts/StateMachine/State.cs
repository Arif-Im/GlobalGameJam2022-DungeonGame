using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected BattleSystem BattleSystem;
    protected bool playOnce = false;

    public State(BattleSystem battleSystem)
    {
        BattleSystem = battleSystem;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator Move()
    {
        yield break;
    }

    public virtual IEnumerator Attack()
    {
        yield break;
    }
    public virtual IEnumerator Heal()
    {
        yield break;
    }
    public virtual IEnumerator Pause()
    {
        yield break;
    }
    public virtual IEnumerator Resume()
    {
        yield break;
    }
}
