using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Grounded
{
    public Dash(MovementSM stateMachine, AttackSM stateMachineATT) : base("Dash", stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    public override void Enter()
    {
        base.Enter();
        if (_sm.character == 0)
        {
            _sm.anim.Play("Air Neutral");
        }
        else if (_sm.character == 1)
        {
            _sm.anim.Play("Tank_Dash");
        }
        _sm.hurtController.dashTime = 0.5f;
        if (_sm.previousState == _sm.movingState)
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = _sm.hurtController.horizontalInput * 26.4f;
            _sm.rb.velocity = velocity;
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.movingState, 0.2f));
        }
        else if (_sm.previousState == _sm.blockingState)
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = _sm.hurtController.horizontalInput * 26.4f;
            _sm.rb.velocity = velocity;
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.blockingState, 0.07f));
        }
        else if (_sm.previousState == _sm.jumpingState)
        {
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.jumpingState, 0.2f));
        }
        else if (_sm.previousState == _sm.neutralState)
        {
            if (_sm.trackOpp.flipped)
            {
                Vector2 velocity = _sm.rb.velocity;
                velocity.x = -1 * 26.4f;
                _sm.rb.velocity = velocity;
            }
            else
            {
                Vector2 velocity = _sm.rb.velocity;
                velocity.x = 1 * 26.4f;
                _sm.rb.velocity = velocity;
            }
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.2f));
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (_sm.previousState == _sm.jumpingState)
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = _sm.hurtController.horizontalInput * 26.4f;
            velocity.y = 0;
            _sm.rb.velocity = velocity;
        }
    }
}
