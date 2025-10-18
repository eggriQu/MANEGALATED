using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto6P : BaseStateATT
{
    public Shoto6P(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto6P", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 18 Frame recovery, 5 frame active
        // TANK : 15 frame recovery, 9 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.3f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.08333333333f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.25f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.15f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.2663626f;
            mvSize.y = 0.6870594f;
            size.x = 0.3650041f;
            size.y = 0.2225184f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.05067687f;
                offset.x = 0.217502f;
            }
            else
            {
                mvOffset.x = -0.05067687f;
                offset.x = -0.217502f;
            }
            mvOffset.y = -0.1514546f;
            offset.y = -0.01599342f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.3613868f;
            mvSize.y = 1.053676f;
            size.x = 0.6972673f;
            size.y = 0.8685637f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.04147291f;
                offset.x = 0.3836336f;
            }
            else
            {
                mvOffset.x = 0.04147291f;
                offset.x = -0.3836336f;
            }
            mvOffset.y = 0.03185397f;
            offset.y = 0.261124f;
        }

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 2;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;

        if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV)
        {

        }
        else
        {
            Vector2 velocity = _smMV.rb.velocity;
            velocity.x = 0;
            _smMV.rb.velocity = velocity;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV && _smMV.hurtController.attackInput
           && !_smMV.hurtController.smMVOpp.hurtController.hit3 && !_smMV.hurtController.downInput)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.neutralState);
        }
        else if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV && _smMV.hurtController.attackInput
            && !_smMV.hurtController.smMVOpp.hurtController.hit3 && _smMV.hurtController.downInput)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.crouchState);
        }
    }
}
