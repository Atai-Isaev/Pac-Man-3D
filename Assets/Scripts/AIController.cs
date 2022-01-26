using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject goal;
    private NavMeshAgent _agent;
    public int state;

    public Transform[] patrolingPoints;
    public Transform[] homePoints;
    private int pointIndex;
    private Vector3 targetPoint;

    private Vector3 randomPoint;
    private static int mapZsize = 30;
    private static int mapXsize = 17;

    private float timeLeft;

    void Start()
    {
       StartNewGame();
    }

	private void StartNewGame(){
		_agent = GetComponent<NavMeshAgent>();
        state = 0;
        pointIndex = 0; 
        targetPoint = patrolingPoints[pointIndex].position;
        _agent.SetDestination(targetPoint);
        timeLeft = 30f;
	}

// Update is called once per frame
    void Update()
    {
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
			case 3: // scare
                WalkInHome();
                break;
        }
       // if (ScoreManager.instance.getScore() > 70)
         //   state = 0;
    }

	private void OnTriggerEnter(Collider other)
    {
		//If trigger with pacman, warp ghost to start point
        if (other.tag.Equals("Player"))
        {
            Debug.Log("Game Over!");
			state = 2;
			_agent.Warp(new Vector3(0,0.51F,0));
        	//randomPoint = new Vector3(0,0,0) + new Vector3(Random.Range(-mapXsize/ 2 , mapXsize / 2), 0, Random.Range(-mapZsize / 2, mapZsize / 2));
            //_agent.SetDestination(randomPoint);
        }
    }

// Chase to specific points
    private void Seek()
    {
		Vector3 location = goal.transform.position;
        timeLeft -= Time.deltaTime;
        
        _agent.SetDestination(location);
        if (timeLeft < 0)
        {
            state = 1;
            timeLeft = 15f;
        }
    }

    private void switchState(object source, ElapsedEventArgs e) 
	{
        if (state == 1) state = 0;
        if (state == 0) state = 1;
    }

	private void WalkInHome(){
		IterateBetweenPoints(homePoints);
	}


// Patrolling between specific points
    private void Patrol()
    {
        IterateBetweenPoints(patrolingPoints);
		CheckTime();
    }

	private void IterateBetweenPoints(Transform[] points){
		if (Vector3.Distance(transform.position, targetPoint) < 1)
        {
            pointIndex++;
        	if(pointIndex == points.Length)
			{
            	pointIndex = 0;
        	}
            targetPoint = points[pointIndex].position;
        }
		_agent.SetDestination(targetPoint);
	}

	private void CheckTime(){
		timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            state = 0;
            timeLeft = 30f;
        }
	}

// Scare and go to random points in map
    private void Scare()
    {
        if(Vector3.Distance(transform.position, randomPoint) < 3 || randomPoint == null)
        {
            randomPoint = new Vector3(0,0,0) + new Vector3(Random.Range(-mapXsize/ 2 , mapXsize / 2), 0, Random.Range(-mapZsize / 2, mapZsize / 2));
            _agent.SetDestination(randomPoint);
        }
    }
}
