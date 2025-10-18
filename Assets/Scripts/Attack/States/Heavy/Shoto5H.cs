using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto5H : BaseStateATT
{
    public Shoto5H(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto5H", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 22 Frame recovery, 5 frame active
        // TANK : 21 frame recovery, 5 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.3666666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.08333333333f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.35f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.08333333333f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.4706478f;
            mvSize.y = 0.5718724f;
            size.x = 0.5910086f;
            size.y = 0.3331318f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.07675533f;
                offset.x = 0.4594123f;
            }
            else
            {
                mvOffset.x = -0.07675533f;
                offset.x = -0.4594123f;
            }
            mvOffset.y = -0.2176069f;
            offset.y = -0.1000376f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.5231835f;
            mvSize.y = 0.8546866f;
            size.x = 1.090526f;
            size.y = 0.4666235f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.03942546f;
                offset.x = 0.5802632f;
            }
            else
            {
                mvOffset.x = -0.03942546f;
                offset.x = -0.5802632f;
            }
            mvOffset.y = -0.06764102f;
            offset.y = 0.01382256f;
        }

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 4;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;
    }
}
