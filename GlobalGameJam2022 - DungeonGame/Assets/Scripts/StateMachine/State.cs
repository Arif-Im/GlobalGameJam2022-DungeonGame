using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE
    {
        IDLE, MOVE, ATTACK
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE stateName;
    protected EVENT stage;
    protected GameObject enemy;
    protected GameObject player;
    protected AStarPathFinder pathFinder;
    protected EnemyMovement enemyMovement;
    protected Unit unit;
    protected Attack attack;
    protected State nextState;

    public State(GameObject _enemy, GameObject _player, AStarPathFinder _pathFinder, EnemyMovement _enemyMovement, Attack _attack, Unit _unit)
    {
        enemy = _enemy;
        player = _player;
        attack = _attack;
        pathFinder = _pathFinder;
        enemyMovement = _enemyMovement;
        unit = _unit;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }

    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        if (Vector2.Distance(player.transform.position, enemy.transform.position) < 1.5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsEnemyTurn()
    {
        if(unit.isCurrentTurn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class IdleState : State
{
    public IdleState(GameObject _enemy, GameObject _player, AStarPathFinder _pathFinder, EnemyMovement _enemyMovement, Attack _attack, Unit _unit) : base(_enemy, _player, _pathFinder, _enemyMovement, _attack, _unit)
    {
        stateName = STATE.IDLE;
    }

    public override void Enter()
    {
        Debug.Log("Enter Idle State");
        base.Enter();
    }

    public override void Update()
    {
        if(IsEnemyTurn())
        {
            if (CanSeePlayer())
            {
                nextState = new AttackState(enemy, player, pathFinder, enemyMovement, attack, unit);
                stage = EVENT.EXIT;
            }
            else
            {
                nextState = new MoveState(enemy, player, pathFinder, enemyMovement, attack, unit);
                stage = EVENT.EXIT;
            }
        }
        else
        {
            Debug.Log("Bing Chilling");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class MoveState : State
{
    public MoveState(GameObject _enemy, GameObject _player, AStarPathFinder _pathFinder, EnemyMovement enemyMovement, Attack _attack, Unit _unit) : base(_enemy, _player, _pathFinder, enemyMovement, _attack, _unit)
    {
        stateName = STATE.MOVE;
    }

    public override void Enter()
    {
        pathFinder.BeginSearch(enemy, player);
        base.Enter();
    }

    public override void Update()
    {
        Debug.Log("Move is Update");
        if (!pathFinder.IsPathCalculated)
        {
            pathFinder.Search(pathFinder.GetLastPos());
        }
        else
        {
            if (!pathFinder.IsStartMoving)
            {
                pathFinder.GetPath();
            }
        }

        if (pathFinder.IsStartMoving)
        {
            Debug.Log("Moving");
            enemyMovement.MoveEnemyAlongPath();
        }

        if (CanSeePlayer())
        {
            nextState = new AttackState(enemy, player, pathFinder, enemyMovement, attack, unit);
            stage = EVENT.EXIT;
        }

        if (!IsEnemyTurn())
        {
            Debug.Log("End Turn");
            nextState = new IdleState(enemy, player, pathFinder, enemyMovement, attack, unit);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        enemyMovement.ResetValues();
        base.Exit();
    }
}

public class AttackState : State
{
    public AttackState(GameObject _enemy, GameObject _player, AStarPathFinder _pathFinder, EnemyMovement _enemyMovement, Attack _attack, Unit _unit) : base(_enemy, _player, _pathFinder, _enemyMovement, _attack, _unit)
    {
        stateName = STATE.ATTACK;
    }

    public override void Enter()
    {
        attack.RoundAttack();
        base.Enter();
    }

    public override void Update()
    {
        if (!CanSeePlayer())
        {
            nextState = new MoveState(enemy, player, pathFinder, enemyMovement, attack, unit);
            stage = EVENT.EXIT;
        }

        if (!IsEnemyTurn())
        {
            nextState = new IdleState(enemy, player, pathFinder, enemyMovement, attack, unit);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
