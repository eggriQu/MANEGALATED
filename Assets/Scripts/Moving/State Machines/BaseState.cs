using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public string name;
    protected StateMachine stateMachine;
    protected StateMachineATT stateMachineATT;

    public BaseState(string name, StateMachine stateMachine, StateMachineATT stateMachineATT)
    {
        this.name = name;
        this.stateMachine = stateMachine;
        this.stateMachineATT = stateMachineATT;
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
