using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyController : AIController
{
    public override void Seek()
    {
        Vector3 location = goal.transform.position;
        _agent.SetDestination(location);
    }
}
