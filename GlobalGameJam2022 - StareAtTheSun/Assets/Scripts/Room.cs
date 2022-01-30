using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] List<Unit> listOfEnemies = new List<Unit>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
        {
            listOfEnemies.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            listOfEnemies.Remove(collision.gameObject.GetComponent<Enemy>());
        }
    }

    public List<Unit> GetListOfEnemies()
    {
        return listOfEnemies;
    }
}
