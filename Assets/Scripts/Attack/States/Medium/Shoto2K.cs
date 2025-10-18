using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto2K : BaseStateATT
{
    public Shoto2K(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto2K", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 10 Frame recovery, 5 frame active
        // TANK : 14 frame recovery, 5 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.1666666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.06666666667f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.2166666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.08333333333f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.6826887f;
            mvSize.y = 0.5899999f;
            size.x = 0.2620617f;
            size.y = 0.8822557f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.06865565f;
                offset.x = 0.06504676f;
            }
            else
            {
                mvOffset.x = 0.06865565f;
                offset.x = -0.06504676f;
            }
            mvOffset.y = -0.25f;
            offset.y = -0.03856909f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.4718034f;
            mvSize.y = 0.946293f;
            size.x = 0.794358f;
            size.y = 0.3428576f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.1706651f;
                offset.x = 0.4321791f;
            }
            else
            {
                mvOffset.x = 0.1706651f;
                offset.x = -0.4321791f;
            }
            mvOffset.y = -0.02374899f;
            offset.y = -0.3292962f;
        }

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 2;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV && _smMV.hurtController.notMedInput && !_smMV.hurtController.smMVOpp.hurtController.hit3)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.crouchState);
        }
    }
}
