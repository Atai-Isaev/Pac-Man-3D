using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject goal;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        _agent.SetDestination(location);
    }
    
    void Persue()
    {
        Vector3 targetDir = goal.transform.position - this.transform.position;
        float lookAhead = targetDir.magnitude / (_agent.speed + 3);
        Seek(goal.transform.position + goal.transform.forward * lookAhead);
    }

// Update is called once per frame
    void Update()
    {
        Persue();
    }
}
