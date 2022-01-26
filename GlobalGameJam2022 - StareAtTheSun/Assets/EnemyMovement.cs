using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int pathMarkerIndex = 0;
    Transform player;

    BattleSystem battleSystem;
    AStarPathFinder pathFinder;
    Unit unit;

    public bool hasDoneBeginSearch = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        battleSystem = FindObjectOfType<BattleSystem>();
        unit = transform.GetComponent<Unit>();
        pathFinder = GetComponent<EnemyAI>().pathFinder;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if (!unit.isCurrentTurn) { return; }

        //if (Vector2.Distance(player.transform.position, transform.position) < 2)
        //{
        //    Debug.Log("Enemy Attacks!");
        //    transform.Find("Blast").GetComponent<ParticleSystem>().Play();
        //    battleSystem.ChangeTurn(unit);
        //}
        //else
        //{
        //    GoToPlayer();
        //}
    }

    private void GoToPlayer()
    {
        if(!hasDoneBeginSearch)
        {
            Debug.Log("Has Done Begin Search = false");
            pathFinder.BeginSearch(this.gameObject, player.gameObject);
            hasDoneBeginSearch = true;
        }

        if(!pathFinder.IsPathCalculated)
        {
            pathFinder.Search(pathFinder.GetLastPos());
        }
        else
        {
            if(!pathFinder.IsStartMoving)
            {
                pathFinder.GetPath();
            }
        }

        if (pathFinder.IsStartMoving)
        {
            Debug.Log("Moving");
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
                //ResetValues();
                battleSystem.ChangeTurn(this.unit);
            }
        }
        else
        {
            //ResetValues();
            battleSystem.ChangeTurn(this.unit);
        }
    }

    public void ResetValues()
    {
        Debug.Log("Has Reset Values");
        pathFinder.ResetValues();
        hasDoneBeginSearch = false;
        pathMarkerIndex = 0;
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }
}
