using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RandomInt_ts : Action
{
    public SharedInt randomNum_zp;

    public override TaskStatus OnUpdate()
    {
        randomNum_zp.Value = Random.Range(0,9);
        return TaskStatus.Success;
    }
}
