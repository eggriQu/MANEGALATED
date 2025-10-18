using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotoGrabHit : BaseStateATT
{
    public ShotoGrabHit(AttackSM stateMachine, MovementSM stateMachineMV) : base("ShotoGrabHit", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.StopAllCoroutines();
        _smMV.StopAllCoroutines();
        if (_smMV.character == 0)
        {
            _sm.anim.Play("Shoto GrabHit");
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 1.583333333f));
        }
        else if (_smMV.character == 1)
        {
            _sm.anim.Play("Tank GrabHit");
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.85f));
        }
        _sm.StartCoroutine(_sm.RecoveryCollider(0, _smMV.grabCollider));
        _smMV.ChangeState(_smMV.inAttack);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector2 velocity = _smMV.rb.velocity;
        velocity.x = 0;
        _smMV.rb.velocity = velocity;
    }
}
