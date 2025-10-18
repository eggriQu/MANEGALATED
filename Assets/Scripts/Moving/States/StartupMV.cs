using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupMV : Grounded
{
    public StartupMV(MovementSM stateMachine, AttackSM stateMachineATT) : base("StartupMV", stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    public override void Enter()
    {
        base.Enter();
        if (_sm.hurtController.grounded && _sm.previousState != _sm.dashState)
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = 0;
            _sm.rb.velocity = velocity;
        }
        else
        {

        }

        if (_sm.previousState == _sm.dashState)
        {
            _sm.StopAllCoroutines();
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
