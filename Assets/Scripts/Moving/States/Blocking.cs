using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : Grounded
{
    public Blocking(MovementSM stateMachine, AttackSM stateMachineATT) : base("Blocking", stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    public override void Enter()
    {
        base.Enter();
        if (_sm.character == 0)
        {
            _sm.anim.Play("Block");
        }
        else if (_sm.character == 1)
        {
            _sm.anim.Play("Tank_Blocking");
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_sm.isPlayer1)
        {
            _sm.hurtController.horizontalInput = Input.GetAxis("Horizontal");
        }
        else
        {
            _sm.hurtController.horizontalInput = Input.GetAxis("HorizontalP2");
        }

        if (_smATT.currentState == _smATT.notAttacking)
        {
            if (_sm.hurtController.horizontalInput == 0)
            {
                stateMachine.ChangeState(_sm.neutralState);
            }
        }
        else
        {
            stateMachine.ChangeState(_sm.neutralState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector2 velocity = _sm.rb.velocity;
        velocity.x = _sm.hurtController.horizontalInput * _sm.speed;
        _sm.rb.velocity = velocity;
    }
}
