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
    public Transform respawnPosition;
    private NavMeshAgent _agent;
    public int state = 2;


    public Transform[] patrolingPoints;
    private int pointIndex;
    private Vector3 targetPoint;

    private Vector3 randomPoint;

    private int mapZsize;
    private int mapXsize;

    private float timeLeft;

    private PlayerController playerController;

    void Start()
    {
        playerController = goal.GetComponent<PlayerController>();
        _agent = GetComponent<NavMeshAgent>();
        pointIndex = 0;
        targetPoint = patrolingPoints[pointIndex].position;
        _agent.SetDestination(targetPoint);

        mapZsize = 30;
        mapXsize = 17;

        timeLeft = 30f;

        randomPoint = new Vector3(0, 0, 0) + new Vector3(Random.Range(-mapXsize / 2, mapXsize / 2), 0, Random.Range(-mapZsize / 2, mapZsize / 2));

        state = 2;
    }

    void Seek(Vector3 location)
    {
        timeLeft -= Time.deltaTime;
        
        _agent.SetDestination(location);
        if (timeLeft < 0)
        {
            state = 1;
            timeLeft = 7f;
        }
    }

    
// Update is called once per frame
    void Update()
    {
        //if(Vector3.Distance(transform.position, respawnPosition.position) < 1 && state == 2)
        //{
        //    scaredPrefab.SetActive(false);
        //    defaultPrefab.SetActive(true);
        //    state = 1;
        //    timeLeft = 15f;
        //}
        if (playerController.onBooster)
        {
            scaredPrefab.SetActive(true);
            defaultPrefab.SetActive(false);
            state = 2;
        }
        else if (state == 2 && !playerController.onBooster)
        {
            scaredPrefab.SetActive(false);
            defaultPrefab.SetActive(true);
            state = 1;
            timeLeft = 7f;
        }

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

        if(timeLeft < 0)
        {
            state = 0;
            timeLeft = 15f;
        }

        _agent.SetDestination(targetPoint);

    }

    void Scare()
    {
        _agent.SetDestination(randomPoint);
        if (Vector3.Distance(transform.position, randomPoint) < 4 || randomPoint == null)
        {
            randomPoint = new Vector3(0,0,0) + new Vector3(Random.Range(-mapXsize/ 2 , mapXsize / 2), 0, Random.Range(-mapZsize / 2, mapZsize / 2)); 
        }
        

    }

    void IteratePatrolpointIndex()
    {
        pointIndex++;
        if(pointIndex == patrolingPoints.Length){
            pointIndex = 0;
        }
        
    }


}
