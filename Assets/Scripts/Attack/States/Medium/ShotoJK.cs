using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotoJK : BaseStateATT
{
    public ShotoJK(AttackSM stateMachine, MovementSM stateMachineMV) : base("ShotoJK", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        // 8 Frame recovery, 4 frame active
        // TANK : 14 frame recovery, 5 frame active
        if (_smMV.character == 0)
        {
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.1333333333f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.06666666667f, _smMV.attCollider));
        }
        else
        {   _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.2333333333f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.08333333333f, _smMV.attCollider));
        }
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        Vector2 offset = _smMV.attCollider.offset;
        Vector2 mvSize = _sm.mvCollider.size;
        Vector2 mvOffset = _sm.mvCollider.offset;

        if (_smMV.character == 0)
        {
            mvSize.x = 0.90356f;
            mvSize.y = 0.3154709f;
            size.x = 0.4506263f;
            size.y = 0.2786037f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.01761317f;
                offset.x = 0.3030218f;
            }
            else
            {
                mvOffset.x = -0.01761317f;
                offset.x = -0.3030218f;
            }
            mvOffset.y = 0.01257235f;
            offset.y = 0.008484155f;
        }
        else if (_smMV.character == 1)
        {
            mvSize.x = 0.4758444f;
            mvSize.y = 0.8048561f;
            size.x = 0.5931808f;
            size.y = 0.2438165f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = -0.01912552f;
                offset.x = 0.3315905f;
            }
            else
            {
                mvOffset.x = 0.01912552f;
                offset.x = -0.3315905f;
            }
            mvOffset.y = 0.04696947f;
            offset.y = -0.1807346f;
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
