using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    State currentState;

    public GameObject enemy;
    public GameObject player;
    public AStarPathFinder pathFinder;
    public EnemyMovement enemyMovement;
    public Attack attack;
    public Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.gameObject;
        pathFinder = FindObjectOfType<AStarPathFinder>();
        currentState = new IdleState(enemy, player, pathFinder, enemyMovement, attack, unit);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player == null) { return; }

        currentState = currentState.Process();
        Debug.Log($"Current State: {currentState}");
    }
}
