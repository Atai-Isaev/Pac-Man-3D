using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlydeController : AIController
{
    public override void Seek()
    {
        Vector3 location = goal.transform.position;
        _agent.SetDestination(location);
    }

	public override void WalkInHome()
    {
        if(ScoreManager.instance.getScore() > 280)
		{
			state = 1;
		}
        IterateBetweenPoints(homePoints);
    }
}
