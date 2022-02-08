using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlydeController : GhostController
{
    private bool inRange;

    protected override void Seek()
    {
        if (Vector3.Distance(transform.position, goal.transform.position) > 8)
        {
            inRange = false;
            Vector3 location = goal.transform.position;
            _agent.SetDestination(location);
        }
        else
        {
            if (!inRange)
            {
                pointIndex = 0;
                targetPoint = patrolingPoints[pointIndex].position;
                _agent.SetDestination(targetPoint);
                inRange = true;
            }
            
        }

        timer -= Time.deltaTime;
        if(timer < 0)
        {
            StartPatrolMode();
        }
    }

    protected override void Activate()
    {
       if(ScoreManager.instance.GetCoinsEaten() >= 60) {
            isActivated = true;
            StartPatrolMode();
       }
    }
}
