using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : BaseState
{
    protected MovementSM _sm;
    protected AttackSM _smATT;
    private bool grounded;
    private float horizontalInput;

    // Some weird bitshift thing, basically means that _groundLayer is the sixth layer in our unity editor
    // (Cuz thats where the ground is)
    private int _groundLayer = 1 << 6;

    public Jumping(MovementSM stateMachine, AttackSM stateMachineATT) : base("Jumping", stateMachine, stateMachineATT)
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
            _sm.anim.Play("Tank_Jump");
        }
        if (_sm.isPlayer1)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("HorizontalP2");
        }

        if (_sm.hurtController.grounded && _sm.previousState != _sm.inAttack)
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.y = 0;
            velocity.y += _sm.jumpForce;
            _sm.rb.velocity = velocity;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (grounded && _sm.currentState != _sm.airHit)
        {
            stateMachine.ChangeState(_sm.neutralState);
        }
        
        if (_sm.isPlayer1)
        {
            if (grounded && Input.GetKey(KeyCode.S))
            {
                stateMachine.ChangeState(_sm.crouchState);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && _sm.hurtController.dashTime == 1)
            {
                stateMachine.ChangeState(_sm.dashState);
            }
        }
        else
        {
            if (grounded && Input.GetKey(KeyCode.DownArrow))
            {
                stateMachine.ChangeState(_sm.crouchState);
            }

            if (Input.GetKeyDown(KeyCode.RightControl) && _sm.hurtController.dashTime == 1)
            {
                stateMachine.ChangeState(_sm.dashState);
            }
        }

        if (_sm.previousState == _sm.inAttack)
        {
            // Velocity stays the same
        }
        else
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = horizontalInput * _sm.speed;
            _sm.rb.velocity = velocity;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        grounded = _sm.rb.velocity.y < Mathf.Epsilon && _sm.rb.IsTouchingLayers(_groundLayer);

        Vector2 size = _sm.mvCollider.size;
        Vector2 offset = _sm.mvCollider.offset;
        if (_sm.character == 0)
        {
            size.x = 0.26f;
            size.y = 0.46f;
            if (!_sm.trackOpp.flipped)
            {
                offset.x = 0.007f;
            }
            else
            {
                offset.x = -0.007f;
            }
            offset.y = -0.02f;
        }
        else if (_sm.character == 1)
        {
            size.x = 0.3472342f;
            size.y = 0.5826733f;
            if (!_sm.trackOpp.flipped)
            {
                offset.x = -0.01480851f;
            }
            else
            {
                offset.x = 0.01480851f;
            }
            offset.y = 0.05769304f;
        }

        _sm.mvCollider.size = size;
        _sm.mvCollider.offset = offset;
    }
}
