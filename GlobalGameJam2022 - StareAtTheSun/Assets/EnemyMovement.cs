using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int pathMarkerIndex = 0;
    Transform player;

    public BattleSystem battleSystem;

    public AStarPathFinder pathFinder;

    bool hasDoneBeginSearch = false;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        pathFinder.BeginSearch();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (battleSystem.state != BattleState.ENEMYTURN) { return; }

        if(Vector2.Distance(transform.position, player.position) < 5)
        {
            GoToPlayer();
        }
        else
        {
            battleSystem.state = BattleState.PLAYERTURN;
        }
    }

    private void GoToPlayer()
    {
        if(!hasDoneBeginSearch)
        {
            pathFinder.BeginSearch();
            hasDoneBeginSearch = true;
        }

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
            MoveEnemyAlongPath();
        }
    }

    public void MoveEnemyAlongPath()
    {
        if(Vector2.Distance(transform.position, pathFinder.startNodeMarker) < 1)
        {
            if (pathMarkerIndex < pathFinder.GetChosenPath().Count)
            {
                if (Vector2.Distance(transform.position, pathFinder.GetChosenPath()[pathMarkerIndex].location.ToVector()) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, pathFinder.GetChosenPath()[pathMarkerIndex].location.ToVector(), 0.1f);
                }
                else
                {
                    pathMarkerIndex++;
                }
            }
            else
            {
                ResetValues();
                battleSystem.state = BattleState.PLAYERTURN;
            }
        }
        else
        {
            ResetValues();
            battleSystem.state = BattleState.PLAYERTURN;
        }
    }

    private void ResetValues()
    {
        pathFinder.IsPathCalculated = false;
        pathFinder.IsStartMoving = false;
        hasDoneBeginSearch = false;
        pathMarkerIndex = 0;
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }
}
