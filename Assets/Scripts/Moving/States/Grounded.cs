using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : BaseState
{
    protected MovementSM _sm;
    protected AttackSM _smATT;
    public bool grounded;

    private int _groundLayer = 1 << 6;

    public Grounded(string name, MovementSM stateMachine, AttackSM stateMachineATT) : base(name, stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.A)
            && !Input.GetKeyDown(KeyCode.S) && grounded && _smATT.currentState == _smATT.notAttacking && _sm.currentState != _sm.grabbed &&
            _sm.currentState != _sm.knockdown && _sm.isPlayer1 && _sm.hurtController.hitStun <= 0)
        {
            stateMachine.ChangeState(_sm.jumpingState);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow)
            && !Input.GetKeyDown(KeyCode.DownArrow) && grounded && _smATT.currentState == _smATT.notAttacking && _sm.currentState != _sm.grabbed &&
            _sm.currentState != _sm.knockdown && !_sm.isPlayer1 && _sm.hurtController.hitStun <= 0)
        {
            stateMachine.ChangeState(_sm.jumpingState);
        }

        if (Input.GetKeyDown(KeyCode.S) && grounded && _smATT.currentState == _smATT.notAttacking && _sm.currentState != _sm.grabbed &&
            _sm.currentState != _sm.knockdown && _sm.currentState != _sm.dashState && _sm.isPlayer1 && _sm.hurtController.hitStun <= 0)
        {
            stateMachine.ChangeState(_sm.crouchState);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && grounded && _smATT.currentState == _smATT.notAttacking && _sm.currentState != _sm.grabbed &&
            _sm.currentState != _sm.knockdown && _sm.currentState != _sm.dashState && !_sm.isPlayer1 && _sm.hurtController.hitStun <= 0)
        {
            stateMachine.ChangeState(_sm.crouchState);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && grounded && _smATT.currentState == _smATT.notAttacking && _sm.currentState != _sm.grabbed &&
            _sm.currentState != _sm.knockdown && _sm.currentState != _sm.crouchState && _sm.isPlayer1 && _sm.hurtController.hitStun <= 0 && _sm.hurtController.dashTime == 1)
        {
            stateMachine.ChangeState(_sm.dashState);
        }
        else if (Input.GetKeyDown(KeyCode.RightControl) && grounded && _smATT.currentState == _smATT.notAttacking && _sm.currentState != _sm.grabbed &&
            _sm.currentState != _sm.knockdown && _sm.currentState != _sm.crouchState && !_sm.isPlayer1 && _sm.hurtController.hitStun <= 0 && _sm.hurtController.dashTime == 1)
        {
            stateMachine.ChangeState(_sm.dashState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        grounded = _sm.rb.velocity.y < Mathf.Epsilon && _sm.rb.IsTouchingLayers(_groundLayer);
    }
}