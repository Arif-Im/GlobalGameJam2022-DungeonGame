using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public float maxHealth;
    public float currentHealth;

    BattleSystem battleSystem;

    public bool isCurrentTurn = false;

    private void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    public void TakeDamage(float damage)
    {
        transform.Find("Blood").GetComponent<ParticleSystem>().Play();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            battleSystem.gameText.text = $"{unitName} has died";
            battleSystem.RemoveUnit(this);
            Destroy(this.gameObject);
        }
    }
}
