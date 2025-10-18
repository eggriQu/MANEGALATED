using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotoJP : BaseStateATT
{
    public ShotoJP(AttackSM stateMachine, MovementSM stateMachineMV) : base("ShotoJP", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 8 Frame recovery, 3 frame active
        // TANK : 8 Frame recovery, 5 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.1333333333f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.05f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.1333333333f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.08333333333f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.26f;
            mvSize.y = 0.46f;
            size.x = 0.33f;
            size.y = 0.34f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.007f;
                offset.x = 0.21f;
            }
            else
            {
                mvOffset.x = -0.007f;
                offset.x = -0.21f;
            }
            mvOffset.y = -0.02f;
            offset.y = -0.22f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.3472342f;
            mvSize.y = 0.5826733f;
            size.x = 0.5165166f;
            size.y = 0.3611929f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.01480851f;
                offset.x = 0.3059425f;
            }
            else
            {
                mvOffset.x = 0.01480851f;
                offset.x = -0.3059425f;
            }
            mvOffset.y = 0.05769304f;
            offset.y = 0.1881895f;
        }

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 0;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV && _smMV.hurtController.attackInput
           && !_smMV.hurtController.smMVOpp.hurtController.hit3 || _smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.airHit && _smMV.hurtController.attackInput
           && !_smMV.hurtController.smMVOpp.hurtController.hit3)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.jumpingState);
        }
    }
}
