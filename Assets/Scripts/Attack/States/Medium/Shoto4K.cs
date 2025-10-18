using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto4K : BaseStateATT
{
    public Shoto4K(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto4K", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 13 Frame recovery, 6 frame active
        // TANK : 11 frame recovery, 10 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.2166666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.1f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.1833333333f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.1666666667f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.3871173f;
            mvSize.y = 0.5597804f;
            size.x = 0.1764482f;
            size.y = 0.429422f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.1086423f;
                offset.x = 0.1477645f;
            }
            else
            {
                mvOffset.x = 0.1086423f;
                offset.x = -0.1477645f;
            }
            mvOffset.y = -0.2150725f;
            offset.y = 0.2295163f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.5231835f;
            mvSize.y = 0.8546866f;
            size.x = 1.124649f;
            size.y = 0.3602334f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.03942546f;
                offset.x = 0.5973246f;
            }
            else
            {
                mvOffset.x = -0.03942546f;
                offset.x = -0.5973246f;
            }
            mvOffset.y = -0.06764102f;
            offset.y = 0.06728393f;
        }

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 4;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.airHit && _smMV.hurtController.notMedInput
           && !_smMV.hurtController.smMVOpp.hurtController.hit3 && !_smMV.hurtController.downInput)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.neutralState);
        }
        else if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.airHit && _smMV.hurtController.notMedInput
            && !_smMV.hurtController.smMVOpp.hurtController.hit3 && _smMV.hurtController.downInput)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.crouchState);
        }
    }
}
