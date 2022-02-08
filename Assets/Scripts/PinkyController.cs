using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyController : GhostController
{
    public String playerDirection;

    protected override void SetUpGhost()
    {
        timer = 7f;
        defaultSpeed = 3;
    }

    protected override void Activate() {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            isActivated = true;
            StartPatrolMode();
        }
    }

    protected override void Seek()
    {
        Quaternion direction = goal.transform.rotation;
        Vector3 targetPosition = goal.transform.position + direction * Vector3.forward * 4f;
        _agent.SetDestination(targetPosition);


        timer -= Time.deltaTime;
        if(timer < 0) {
            StartPatrolMode();
        }
    }

}
