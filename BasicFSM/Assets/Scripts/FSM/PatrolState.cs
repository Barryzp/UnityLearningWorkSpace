using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState :FSMState
{
    private Transform[] wayPoints;
    private float curRotSpeed = 0.1f;
    private float curSpeed = 100.0f;
    private Vector3 destPos = Vector3.zero;

    private void FindNextPoint(){
        Debug.Log("find next point.");
    }

    public PatrolState(Transform[] wanderPoints){
        wayPoints = wanderPoints;
        curRotSpeed = 0.3f;
        curSpeed = 3;
    }

    public override void Reason(Transform player, Transform npc)
    {
        var distanceFromPlayer = Vector3.Distance(npc.position,player.position);
        var distanceFromWanderP = Vector3.Distance(npc.position,destPos);

        if(distanceFromPlayer <= 1){

        }
    }

    public override void Act(Transform player, Transform npc)
    {
    }
}
