using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto2H : BaseStateATT
{
    public Shoto2H(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto2H", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 12 Frame recovery, 3 frame active
        // TANK : 17 frame recovery, 5 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.2f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.05f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.2833333333f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.08333333333f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.66f;
            mvSize.y = 0.59f;
            size.x = 0.95f;
            size.y = 0.29f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.08f;
                offset.x = 0.26f;
            }
            else
            {
                mvOffset.x = 0.08f;
                offset.x = -0.26f;
            }
            mvOffset.y = -0.25f;
            offset.y = -0.28f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.4176214f;
            mvSize.y = 1.540101f;
            size.x = 0.6308233f;
            size.y = 1.27348f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.3158921f;
                offset.x = 0.02777193f;
            }
            else
            {
                mvOffset.x = -0.3158921f;
                offset.x = -0.02777193f;
            }
            mvOffset.y = 0.263401f;
            offset.y = 0.1272721f;
        }

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 3;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;
    }
}
