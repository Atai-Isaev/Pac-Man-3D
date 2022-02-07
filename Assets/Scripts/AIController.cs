using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject goal;
    public GameObject ghost;
    public GameObject defaultPrefab;
    public GameObject scaredPrefab;
    public GameObject deadPrefab;
    protected NavMeshAgent _agent;
    public int state;

    public Transform[] patrolingPoints;
    public Transform[] homePoints;
    public int pointIndex;
    public Vector3 targetPoint;

    private Vector3 randomPoint;
    private static int mapZsize = 30;
    private static int mapXsize = 17;

    private PlayerController playerController;

    public float patrolTimeLeft;
    public float scareTimeLeft;
    public float chaseTimeLeft;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        playerController = goal.GetComponent<PlayerController>();
        StartNewGame();
    }

    public virtual void StartNewGame()
    {
        state = 3;
        pointIndex = 1;
        patrolTimeLeft = 30f;
        scareTimeLeft = 7f;
        chaseTimeLeft = 15f;
        targetPoint = homePoints[0].position;
        _agent.Warp(targetPoint);

    }


    // Update is called once per frame
    void Update()
    {
        if (playerController.onBooster && state != 4 && state != 3)
        {
            defaultPrefab.SetActive(false);
            scaredPrefab.SetActive(true);
            scareTimeLeft = 7f;
            randomPoint = new Vector3(0, 0, 0) + new Vector3(Random.Range(-mapXsize / 2, mapXsize / 2), 0, Random.Range(-mapZsize / 2, mapZsize / 2));
            state = 2;
        }
        switch (state)
        {
            case 0: // chasing
                Seek();
                break;
            case 1: // patrolling
                Patrol();
                break;
            case 2: // scare
                Scare();
                break;
            case 3: // in home
                WalkInHome();
                break;
            case 4: // dead
                Dead();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If trigger with pacman, warp ghost to start point
        if (other.tag.Equals("Player"))
        {
            if (playerController.onBooster)
            {
                scaredPrefab.SetActive(false);
                deadPrefab.SetActive(true);
                _agent.SetDestination(homePoints[0].position);
                state = 4;
            }
            else
            {
                state = 3;
                _agent.Warp(homePoints[0].position);
            }
        }
    }

    // Chase to specific points
    public virtual void Seek()
    {
        Vector3 location = goal.transform.position;
        _agent.SetDestination(location);

        chaseTimeLeft -= Time.deltaTime;
        if (chaseTimeLeft < 0)
        {
            ChangeToPatrol();
        }
    }

    public virtual void WalkInHome()
    {
        if (homePoints.Length == 1)
        {
            ChangeToPatrol();
            return;
        }
        if (ScoreManager.instance.getScore() > 30)
        {
            ChangeToPatrol();
        }
        IterateBetweenPoints(homePoints);
    }


    // Patrolling between specific points
    private void Patrol()
    {
        patrolTimeLeft -= Time.deltaTime;
        if (patrolTimeLeft < 0)
        {
            state = 0;
            chaseTimeLeft = 15f;
        }
        IterateBetweenPoints(patrolingPoints);
    }

    protected void IterateBetweenPoints(Transform[] points)
    {

        if (Vector3.Distance(transform.position, targetPoint) < 1)
        {
            pointIndex++;
            if (pointIndex == points.Length)
            {
                pointIndex = 0;
            }
            targetPoint = points[pointIndex].position;
            _agent.SetDestination(targetPoint);
        }
    }

    // Scare and go to random points in map
    private void Scare()
    {
        scareTimeLeft -= Time.deltaTime;
        if (scareTimeLeft < 0)
        {
            if (patrolTimeLeft < 0)
            {
                state = 0;
            }
            else
            {
                ChangeToPatrol();
            }
            scaredPrefab.SetActive(false);
            defaultPrefab.SetActive(true);
            scareTimeLeft = 7f;
        }

        if (Vector3.Distance(transform.position, randomPoint) < 4 || randomPoint == null)
        {
            randomPoint = new Vector3(0, 0, 0) + new Vector3(Random.Range(-mapXsize / 2, mapXsize / 2), 0, Random.Range(-mapZsize / 2, mapZsize / 2));
            _agent.SetDestination(randomPoint);
        }
    }

    public void Dead()
    {
        if (Vector3.Distance(transform.position, homePoints[0].position) < 1)
        {
            defaultPrefab.SetActive(true);
            deadPrefab.SetActive(false);
            state = 3;
        }
    }

    protected void ChangeToPatrol()
    {
        state = 1;
        patrolTimeLeft = 15f;
        _agent.SetDestination(targetPoint);
    }
}
