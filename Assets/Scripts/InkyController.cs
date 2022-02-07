using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyController : AIController
{
    public override void Seek()
    {
        chaseTimeLeft -= Time.deltaTime;
        if (chaseTimeLeft < 0)
        {
            state = 1;
            patrolTimeLeft = 7f;
        }
        Vector3 location = goal.transform.position;
        _agent.SetDestination(location);
    }
}
