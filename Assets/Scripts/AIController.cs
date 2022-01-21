﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject goal;
    private NavMeshAgent _agent;
    private int state;
    public Transform[] patrolingPoints;
    private int pointIndex;
    private Vector3 targetPoint;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        state = 1;
    }

    void Seek(Vector3 location)
    {
        _agent.SetDestination(location);
    }
    
// Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0: // chasing
                Seek(goal.transform.position);
                break;
            case 1: // patroling
                Patrol();
                break;
        }
        
    }

    void Patrol()
    {
        IteratePatrolpointIndex();
        targetPoint = patrolingPoints[pointIndex].position;
        _agent.SetDestination(targetPoint);
    }

    void IteratePatrolpointIndex()
    {
        pointIndex++;
        if(pointIndex == patrolingPoints.Length){
            pointIndex = 0;
        }
    }
}
