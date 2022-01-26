using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class BattleSystem : StateMachine
{
    public TextMeshProUGUI gameText;

    [SerializeField] List<Unit> unitList;

    private void Awake()
    {
        
    }

    private void Start()
    {
        ChangeTurn(unitList[0]);
    }

    public void ChangeTurn(Unit unit)
    {
        int currentUnit = 0;
        for (int i = 0; i < unitList.Count; i++)
        {
            if (unit == unitList[i])
            {
                currentUnit = i;
            }
            unitList[i].isCurrentTurn = false;
        }

        if(currentUnit >= unitList.Count-1)
        {
            unitList[0].isCurrentTurn = true;
        }
        else
        {
            unitList[currentUnit + 1].isCurrentTurn = true;
        }
    }


    public void RemoveUnit(Unit unit)
    {
        int currentUnit = 0;
        for (int i = 0; i < unitList.Count; i++)
        {
            if (unit == unitList[i])
            {
                currentUnit = i;
            }
        }
        unitList.RemoveAt(currentUnit);
    }
}
