using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral : Grounded
{
    public Neutral(MovementSM stateMachine, AttackSM stateMachineATT) : base("Neutral", stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.hurtController.hit1 = false;
        _sm.hurtController.hit2 = false;
        _sm.hurtController.hit3 = false;
        if (_sm.character == 0)
        {
            _sm.anim.Play("Neutral");
        }
        else if (_sm.character == 1)
        {
            _sm.anim.Play("Tank_Neutral");
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_smATT.currentState == _smATT.notAttacking && !_sm.trackOpp.flipped)
        {
            if (_sm.hurtController.horizontalInput > 0 && grounded)
            {
                stateMachine.ChangeState(_sm.movingState);
            }
            else if (_sm.hurtController.horizontalInput < 0 && grounded)
            {
                stateMachine.ChangeState(_sm.blockingState);
            }
        }

        if (_smATT.currentState == _smATT.notAttacking && _sm.trackOpp.flipped)
        {
            if (_sm.hurtController.horizontalInput > 0 && grounded)
            {
                stateMachine.ChangeState(_sm.blockingState);
            }
            else if (_sm.hurtController.horizontalInput < 0 && grounded)
            {
                stateMachine.ChangeState(_sm.movingState);
            }
        }

        if (!grounded)
        {
            stateMachine.ChangeState(_sm.jumpingState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector2 velocity = _sm.rb.velocity;
        velocity.x = 0;
        _sm.rb.velocity = velocity;

        Vector2 size = _sm.mvCollider.size;
        Vector2 offset = _sm.mvCollider.offset;
        if (_sm.character == 0)
        {
            size.x = 0.1912344f;
            size.y = 0.7444203f;
            if (!_sm.trackOpp.flipped)
            {
                offset.x = -0.01070077f;
            }
            else
            {
                offset.x = 0.02f;
            }
            offset.y = -0.1227742f;
        }
        else if (_sm.character == 1)
        {
            size.x = 0.3613868f;
            size.y = 0.9037118f;
            if (!_sm.trackOpp.flipped)
            {
                offset.x = -0.04147291f;
            }
            else
            {
                offset.x = 0.04147291f;
            }
            offset.y = -0.04312845f;
        }

        _sm.mvCollider.size = size;
        _sm.mvCollider.offset = offset;
    }
}
