using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyController : GhostController
{
    public GameObject blinky;

    protected override void Seek()
    {
        Quaternion direction = goal.transform.rotation;
        Vector3 targetPosition = goal.transform.position + direction * Vector3.forward * 2f;
		Vector3 oargetPosition = targetPosition;
		
		
		float x = targetPosition.x - blinky.transform.position.x; 
		float z = targetPosition.z - blinky.transform.position.z;
        targetPosition = new Vector3( targetPosition.x + x , targetPosition.y, targetPosition.z + z);

		Debug.Log( blinky.transform.position + "  "+ oargetPosition + "  "+ targetPosition);

        _agent.SetDestination(targetPosition);
    }

    protected override void Activate() {
        if(ScoreManager.instance.GetCoinsEaten() >= 30) {
            isActivated = true;
            StartPatrolMode();
        }
    }

}
