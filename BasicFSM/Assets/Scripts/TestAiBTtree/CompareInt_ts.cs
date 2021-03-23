using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CompareInt_ts : Conditional
{
    public SharedInt judgeInt1;
    public SharedInt judgeInt2;
    public override TaskStatus OnUpdate()
    {
        if (judgeInt1.Value > judgeInt2.Value){
            return TaskStatus.Success;
        }else{
            return TaskStatus.Failure;
        }
    }
}

/*
    四元数：轴角对


*/