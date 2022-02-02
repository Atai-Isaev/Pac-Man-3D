using System;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject goal;
    private NavMeshAgent _agent;
    public int state = 0;

    public Transform[] patrolingPoints;
    private int pointIndex;
    private Vector3 targetPoint;

    private Vector3 randomPoint;

    private int mapZsize;
    private int mapXsize;

    private float timeLeft;

    public Transform respawnPosition;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        pointIndex = 0;
        targetPoint = patrolingPoints[pointIndex].position;
        _agent.SetDestination(targetPoint);

        mapZsize = 30;
        mapXsize = 17;

        timeLeft = 30f;
    }

    void Seek(Vector3 location)
    {
        timeLeft -= Time.deltaTime;

        _agent.SetDestination(location);
        if (timeLeft < 0)
        {
            state = 1;
            timeLeft = 15f;
        }
    }


    void switchState(object source, ElapsedEventArgs e)
    {
        if (state == 1) state = 0;
        if (state == 0) state = 1;
    }


// Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0: // chasing
                Seek(goal.transform.position);
                break;
            case 1: // patrolling
                Patrol();
                break;
            case 2: // scare
                Scare();
                break;
        }
        // if (ScoreManager.instance.getScore() > 70)
        //   state = 0;
    }

    void Patrol()
    {
        timeLeft -= Time.deltaTime;
        if (Vector3.Distance(transform.position, targetPoint) < 1)
        {
            IteratePatrolpointIndex();
            targetPoint = patrolingPoints[pointIndex].position;
        }

        if (timeLeft < 0)
        {
            state = 0;
            timeLeft = 30f;
        }

        _agent.SetDestination(targetPoint);
    }

    void Scare()
    {
        if (Vector3.Distance(transform.position, randomPoint) < 3 || randomPoint == null)
        {
            randomPoint = new Vector3(0, 0, 0) + new Vector3(Random.Range(-mapXsize / 2, mapXsize / 2), 0,
                Random.Range(-mapZsize / 2, mapZsize / 2));
            _agent.SetDestination(randomPoint);
        }
    }

    void IteratePatrolpointIndex()
    {
        pointIndex++;
        if (pointIndex == patrolingPoints.Length)
        {
            pointIndex = 0;
        }
    }

    // void Reset()
    // {
    //     transform.position = respawnPosition.position;
    // }
}