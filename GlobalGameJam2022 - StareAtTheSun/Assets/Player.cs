using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    Cinemachine.CinemachineVirtualCamera vCM;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        vCM = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if(isCurrentTurn)
        {
            vCM.Follow = this.gameObject.transform;
            vCM.LookAt = this.gameObject.transform;
        }
    }
}
