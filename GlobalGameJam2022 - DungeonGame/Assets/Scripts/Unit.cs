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

    protected BattleSystem battleSystem;

    public bool isCurrentTurn = false;

    public void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    public virtual void TakeDamage(float damage)
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
