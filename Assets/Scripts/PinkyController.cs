using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyController : AIController
{
    private float walkInHomeTimeLeft = 7f;
    public override void Seek()
    {
        Vector3 location = goal.transform.position;
        _agent.SetDestination(location);
    }
    
    public override void WalkInHome()
    {
        walkInHomeTimeLeft -= Time.deltaTime;
        if(walkInHomeTimeLeft < 0)
        {
            state = 1;
        }
        IterateBetweenPoints(homePoints);
    }
    
    public override void StartNewGame()
    {
        state = 3;
        pointIndex = 0; 
        patrolTimeLeft = 30f;
        scareTimeLeft = 7f;
        walkInHomeTimeLeft = 7f;
        targetPoint = homePoints[0].position;
        _agent.Warp(targetPoint);
    }
}
