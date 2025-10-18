using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotoJH : BaseStateATT
{
    public ShotoJH(AttackSM stateMachine, MovementSM stateMachineMV) : base("ShotoJH", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 19 Frame recovery, 6 frame active
        // TANK : 16 frame recovery, 7 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.3166666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.1f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.2666666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.1166666667f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.74f;
            mvSize.y = 0.81f;
            size.x = 0.76f;
            size.y = 0.22f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.074f;
                offset.x = 0.06f;
            }
            else
            {
                mvOffset.x = -0.074f;
                offset.x = -0.06f;
            }
            mvOffset.y = 0.035f;
            offset.y = -0.39f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.8578608f;
            mvSize.y = 0.5225092f;
            size.x = 1.011412f;
            size.y = 0.3902638f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.0330112f;
                offset.x = 0.01899678f;
            }
            else
            {
                mvOffset.x = -0.0330112f;
                offset.x = -0.01899678f;
            }
            mvOffset.y = 0.03471063f;
            offset.y = -0.3610033f;
        }

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 2;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;
    }
}
