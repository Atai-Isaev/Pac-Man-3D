using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyController : GhostController
{
    protected override void Seek()
    {
        Vector3 location = goal.transform.position;
        _agent.SetDestination(location);

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            StartPatrolMode();
        }
    }

    protected override void Activate()
    {
        isActivated = true;
        StartPatrolMode();
    }
}
