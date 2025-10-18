using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotoGrab : BaseStateATT
{
    public ShotoGrab(AttackSM stateMachine, MovementSM stateMachineMV) : base("ShotoGrab", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        Vector2 size = _smMV.grabCollider.size;
        Vector2 offset = _smMV.grabCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.3756327f;
            mvSize.y = 0.8498073f;
            size.x = 0.2745727f;
            size.y = 0.6153486f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.1197134f;
                offset.x = 0.2875851f;
            }
            else
            {
                mvOffset.x = -0.1197134f;
                offset.x = -0.2875851f;
            }
            mvOffset.y = -0.1200963f;
            offset.y = -0.1836452f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.7354076f;
            mvSize.y = 0.5736165f;
            size.x = 0.8342228f;
            size.y = 0.6771281f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.2996009f;
                offset.x = 0.56741f;
            }
            else
            {
                mvOffset.x = -0.2996009f;
                offset.x = -0.56741f;
            }
            mvOffset.y = 0.1379241f;
            offset.y = -0.1527554f;
        }

        _smMV.grabCollider.size = size;
        _smMV.grabCollider.offset = offset;

        _sm.attackLevel = 4;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;

        // Use active time for dis
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.RecoveryCollider(0.05f, _smMV.grabCollider));
        }
        else if (_smMV.character == 1)
        {
            _sm.StartCoroutine(_sm.RecoveryCollider(0.06666666667f, _smMV.grabCollider));
        }

        if (_sm.grabController.grabHit)
        {

        }
        else
        {
            // 40 Frame recovery
            // TANK: 36 frame recovery
            if (_smMV.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.6666666667f));
            }
            else if (_smMV.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.6f));
            }
            _smMV.ChangeState(_smMV.inAttack);
        }
    }
}
