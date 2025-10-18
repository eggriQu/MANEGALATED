using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateATT
{
    public string name;
    protected StateMachineATT stateMachineATT;
    protected StateMachine stateMachine;
    protected AttackSM _sm;
    protected MovementSM _smMV;

    public BaseStateATT(string name, StateMachineATT stateMachineATT, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachineATT = stateMachineATT;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void UpdateLogic()
    {

    }

    public virtual void UpdatePhysics()
    {

    }

    public virtual void Exit()
    {

    }
}
