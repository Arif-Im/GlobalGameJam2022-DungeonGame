using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int pathMarkerIndex = 0;

    BattleSystem battleSystem;
    AStarPathFinder pathFinder;
    Unit unit;

    public bool hasDoneBeginSearch = false;

    // Start is called before the first frame update
    void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
        unit = transform.GetComponent<Unit>();
        pathFinder = GetComponent<EnemyAI>().pathFinder;
    }

    public void MoveEnemyAlongPath()
    {
        if(Vector2.Distance(transform.position, pathFinder.startNodeMarker) < 1)
        {
            if (pathMarkerIndex < pathFinder.GetChosenPath().Count)
            {
                if (Vector2.Distance(transform.position, pathFinder.GetChosenPath()[pathMarkerIndex].location.ToVector()) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, pathFinder.GetChosenPath()[pathMarkerIndex].location.ToVector(), 1000 * Time.deltaTime);
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
