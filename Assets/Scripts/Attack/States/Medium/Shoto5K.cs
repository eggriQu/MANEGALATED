using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto5K : BaseStateATT
{
    public Shoto5K(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto5K", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        // 16 Frame recovery, 5 frame active
        // TANK : 12 frame recovery, 9 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.2666666667f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.08333333333f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.2f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.15f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.2137681f;
            mvSize.y = 0.5447862f;
            size.x = 0.2418767f;
            size.y = 0.3708145f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.02687041f;
                offset.x = 0.1400157f;
            }
            else
            {
                mvOffset.x = 0.02687041f;
                offset.x = -0.1400157f;
            }
            mvOffset.y = -0.2176069f;
            offset.y = -0.106655f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.5231835f;
            mvSize.y = 0.8546866f;
            size.x = 0.59788f;
            size.y = 0.4891629f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.03942546f;
                offset.x = 0.33394f;
            }
            else
            {
                mvOffset.x = -0.03942546f;
                offset.x = -0.33394f;
            }
            mvOffset.y = -0.06764102f;
            offset.y = -0.25504f;
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
        if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV && _smMV.hurtController.notMedInput
           && !_smMV.hurtController.smMVOpp.hurtController.hit3 && !_smMV.hurtController.downInput)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.neutralState);
        }
        else if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV && _smMV.hurtController.notMedInput
            && !_smMV.hurtController.smMVOpp.hurtController.hit3 && _smMV.hurtController.downInput)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.crouchState);
        }
    }
}
