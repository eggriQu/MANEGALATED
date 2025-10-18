using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto2P : BaseStateATT
{
    public Shoto2P(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto2P", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 8 Frame recovery, 4 frame active
        // TANK : 9 Frame recovery, 4 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.1333333333f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.06666666667f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.15f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.06666666667f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.3f;
            mvSize.y = 0.3698482f;
            size.x = 0.44f;
            size.y = 0.17f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.01000001f;
                offset.x = 0.27f;
            }
            else
            {
                mvOffset.x = 0.01000001f;
                offset.x = -0.27f;
            }
            mvOffset.y = -0.3100759f;
            offset.y = -0.2f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.3289065f;
            mvSize.y = 0.5057087f;
            size.x = 0.598455f;
            size.y = 0.2451137f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.03625041f;
                offset.x = 0.3469117f;
            }
            else
            {
                mvOffset.x = -0.03625041f;
                offset.x = -0.3469117f;
            }
            mvOffset.y = -0.2421457f;
            offset.y = -0.1463919f;
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
        if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV && _smMV.hurtController.attackInput && !_smMV.hurtController.smMVOpp.hurtController.hit3)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.crouchState);
        }
    }
}
