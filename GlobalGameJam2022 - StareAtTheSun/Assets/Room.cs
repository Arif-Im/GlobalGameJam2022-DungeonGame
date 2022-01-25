using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    List<Unit> listOfEnemies = new List<Unit>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
        {
            listOfEnemies.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }

    public List<Unit> GetListOfEnemies()
    {
        return listOfEnemies;
    }
}
