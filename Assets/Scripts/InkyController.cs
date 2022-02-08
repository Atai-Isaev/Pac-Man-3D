using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyController : GhostController
{
    public GameObject blinky;

    protected override void Seek()
    {
        Quaternion direction = goal.transform.rotation;
        Vector3 targetPosition = goal.transform.position + direction * Vector3.forward * 2f;

        targetPosition = blinky.transform.position + (targetPosition - blinky.transform.position) * 2;

        _agent.SetDestination(targetPosition);
    }

    protected override void Activate() {
        if(ScoreManager.instance.GetCoinsEaten() > 30) {
            isActivated = true;
            StartPatrolMode();
        }
    }

}
