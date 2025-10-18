using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto6H : BaseStateATT
{
    public Shoto6H(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto6H", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 22 Frame recovery, 5 frame active
        // TANK : 20 frame recovery, 6 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.3666666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.08333333333f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.3333333333f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.1f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.2691204f;
            mvSize.y = 0.834125f;
            size.x = 0.5727006f;
            size.y = 0.4758707f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.1014562f;
                offset.x = 0.2989456f;
            }
            else
            {
                mvOffset.x = 0.1014562f;
                offset.x = -0.2989456f;
            }
            mvOffset.y = -0.07792181f;
            offset.y = -0.2367205f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.64876f;
            mvSize.y = 0.5648946f;
            size.x = 0.813614f;
            size.y = 0.4891629f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.4370845f;
                offset.x = 0.9119141f;
            }
            else
            {
                mvOffset.x = -0.4370845f;
                offset.x = -0.9119141f;
            }
            mvOffset.y = -0.212537f;
            offset.y = -0.25504f;
        }

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 4;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;
    }
}
