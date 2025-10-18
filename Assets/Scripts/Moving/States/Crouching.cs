using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : Grounded
{
    public Crouching(MovementSM stateMachine, AttackSM stateMachineATT) : base("Crouching", stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    public override void Enter()
    {
        base.Enter();
        if (_sm.character == 0)
        {
            _sm.anim.Play("Crouch");
        }
        else if (_sm.character == 1)
        {
            _sm.anim.Play("Tank_Crouch");
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (!Input.GetKey(KeyCode.S) && grounded && _sm.isPlayer1)
        {
            stateMachine.ChangeState(_sm.neutralState);
        }
        else if (!Input.GetKey(KeyCode.DownArrow) && grounded && !_sm.isPlayer1)
        {
            stateMachine.ChangeState(_sm.neutralState);
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
            size.x = 0.3f;
            size.y = 0.3698482f;
            if (!_sm.trackOpp.flipped)
            {
                offset.x = -0.01000001f;
            }
            else
            {
                offset.x = 0.01000001f;
            }
            offset.y = -0.3100759f;
        }
        else if (_sm.character == 1)
        {
            size.x = 0.3289065f;
            size.y = 0.5057087f;
            if (!_sm.trackOpp.flipped)
            {
                offset.x = 0.03625041f;
            }
            else
            {
                offset.x = -0.03625041f;
            }
            offset.y = -0.2421457f;
        }

        _sm.mvCollider.size = size;
        _sm.mvCollider.offset = offset;
    }
}
