using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto5P : BaseStateATT
{
    public Shoto5P(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto5P", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 7 Frame recovery, 4 frame active
        // TANK : 8 frame recovery, 7 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.1166666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.06666666667f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.1166666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.1166666667f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.1912344f;
            mvSize.y = 0.7444203f;
            size.x = 0.41f;
            size.y = 0.13f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.01070077f;
                offset.x = 0.24f;
            }
            else
            {
                mvOffset.x = 0.02f;
                offset.x = -0.24f;
            }
            mvOffset.y = -0.1227742f;
            offset.y = -0.002f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.3613868f;
            mvSize.y = 0.9037118f;
            size.x = 0.5244268f;
            size.y = 0.296665f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.04147291f;
                offset.x = 0.2972134f;
            }
            else
            {
                mvOffset.x = 0.04147291f;
                offset.x = -0.2972134f;
            }
            mvOffset.y = -0.04312845f;
            offset.y = 0.05396961f;
        }

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 0;
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
