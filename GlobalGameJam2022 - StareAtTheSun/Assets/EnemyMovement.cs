using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int pathMarkerIndex = 1;
    Transform player;

    public BattleSystem battleSystem;

    public AStarPathFinder pathFinder;


    private void Awake()
    {
        GridMovementController.OnEnemyState += EnemyState;
    }

    // Start is called before the first frame update
    void Start()
    {
        pathFinder.BeginSearch();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(battleSystem.state != BattleState.ENEMYTURN) { return; }

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

        //if (pathFinder.IsStartMoving)
        //{
        //    MoveEnemyAlongPath();
        //}
    }
    public void MoveEnemyAlongPath()
    {
        Debug.Log("Enemy Move");

        if(Vector2.Distance(transform.position, pathFinder.startNodeMarker.transform.position) < 1)
        {
            if (pathMarkerIndex <= pathFinder.GetChosenPath().Count - 1)
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
                transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                battleSystem.state = BattleState.PLAYERTURN;
            }
        }
        else
        {
            transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            battleSystem.state = BattleState.PLAYERTURN;
        }
    }

    void EnemyState()
    {

        Destroy(GameObject.Find("Start(Clone)"));
        Destroy(GameObject.Find("End(Clone)"));

        GameObject[] currentPaths = GameObject.FindGameObjectsWithTag("Path");
        if (currentPaths.Length >= 1)
        {
            foreach (GameObject currentPath in currentPaths)
            {
                Destroy(currentPath);
            }
        }

        battleSystem.state = BattleState.ENEMYTURN;
        pathFinder.IsPathCalculated = false;
        pathFinder.IsStartMoving = false;
        pathMarkerIndex = 1;
        pathFinder.BeginSearch();
    }
}
