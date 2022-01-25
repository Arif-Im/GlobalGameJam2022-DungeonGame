using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public float maxHealth;
    public float currentHealth;

    public void TakeDamage(float damage)
    {
        transform.Find("Blood").GetComponent<ParticleSystem>().Play();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
