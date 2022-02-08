using System.Threading.Tasks;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject goal;
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

    private bool isScared;
    private bool isDead;
    protected bool isActivated;

    private static float patrolDuration = 7f;
    private static float chaseDuration = 15f;

    public float timer;

    void Start()
    {
        isScared = false;
        isDead = false;
        isActivated = false;
        _agent = GetComponent<NavMeshAgent>();
        state = -1;
        SetUpGhost();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case -1: //not active
                Activate();
                break;
            case 0: // chasing
                Seek();
                break;
            case 1: // patrolling
                Patrol();
                break;
            case 2: // scare
                Scare();
                break;
            case 3: //dead
                Dead();
                break;
        }
    }

    protected virtual void SetUpGhost() {

    }


    protected virtual void Activate() {

    }

    // Chase to specific points
    protected virtual void Seek()
    {

    }

    private void Patrol()
    {
        timer -= Time.deltaTime;
        if(timer < 0) {
            StartSeekMode();
        }
        if (Vector3.Distance(transform.position, targetPoint) < 1)
        {
            pointIndex++;
            if (pointIndex == patrolingPoints.Length)
            {
                pointIndex = 0;
            }
            targetPoint = patrolingPoints[pointIndex].position;
            _agent.SetDestination(targetPoint);
        }
    }

    private void Scare()
    { 
        if (Vector3.Distance(transform.position, randomPoint) < 4 || randomPoint == null)
        {
            randomPoint = new Vector3(0, 0, 0) + new Vector3(Random.Range(-mapXsize / 2, mapXsize / 2), 0, Random.Range(-mapZsize / 2, mapZsize / 2));
            _agent.SetDestination(randomPoint);
        }
    }

    private void Dead() {
        if(Vector3.Distance(transform.position, homePoints[0].position) < 1) {
            StartPatrolMode();
        }
    }

    protected void IterateBetweenPoints(Transform[] points)
    {
        
    }

    public void StartSeekMode() {
        EndCurrentMode();
        timer = chaseDuration;
        state = 0;
        defaultPrefab.SetActive(true);
    }


    public void StartPatrolMode()
    {
        EndCurrentMode();
        timer = patrolDuration;
        state = 1;
        defaultPrefab.SetActive(true);
        pointIndex = 0;
        targetPoint = patrolingPoints[0].position;
        _agent.SetDestination(targetPoint);
    }

    public void StartScaredMode()
    {
        EndCurrentMode();
        state = 2;
        isScared = true;
        scaredPrefab.SetActive(true);
        randomPoint = new Vector3(Random.Range(-mapXsize / 2, mapXsize / 2), 0, Random.Range(-mapZsize / 2, mapZsize / 2));
        _agent.SetDestination(randomPoint);
    }

   
    public void StartDeadMode()
    {
        EndCurrentMode();
        deadPrefab.SetActive(true);
        isDead = true;
        _agent.SetDestination(homePoints[0].position);
        _agent.speed = 5;
        state = 3;
    }

    private void EndDeadMode()
    {
        deadPrefab.SetActive(false);
        isDead = false;
        _agent.speed = 2;
    }

    public void EndScaredMode()
    {
        isScared = false;
        scaredPrefab.SetActive(false);
    }

    public void EndPatrolModeOrSeekMode()
    {
        defaultPrefab.SetActive(false);
    }


    private void EndCurrentMode()
    {
        switch (state)
        {
            case 0: // chasing
                EndPatrolModeOrSeekMode();
                break;
            case 1: // patrolling
                EndPatrolModeOrSeekMode();
                break;
            case 2: // scare
                EndScaredMode();
                break;
            case 3: //dead
                EndDeadMode();
                break;
        }
    }

    public bool IsScared()
    {
        return isScared;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool IsActivated()
    {
        return isActivated;
    }
}
