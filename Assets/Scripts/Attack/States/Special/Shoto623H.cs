using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto623H : BaseStateATT
{
    public Shoto623H(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto623H", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        // 16 Frame recovery
        _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.2666666667f));
        _smMV.ChangeState(_smMV.inAttack);

        Vector2 size = _smMV.attCollider.size;
        size.x = 0.2955035f;
        size.y = 0.9477304f;
        Vector2 mvSize = _sm.mvCollider.size;
        mvSize.x = 0.2157515f;
        mvSize.y = 0.8267691f;
        Vector2 mvOffset = _sm.mvCollider.offset;
        Vector2 offset = _smMV.attCollider.offset;
        if (!_sm.trackOpp.flipped)
        {
            offset.x = 0.2439458f;
            mvOffset.x = 0.1300519f;
        }
        else
        {
            offset.x = -0.2439458f;
            mvOffset.x = -0.1300519f;
        }
        offset.y = 0.5117342f;
        mvOffset.y = 0.4944926f;

        _smMV.attCollider.size = size;
        _smMV.attCollider.offset = offset;

        _sm.attackLevel = 4;
        _smMV.mvCollider.size = mvSize;
        _smMV.mvCollider.offset = mvOffset;

        // Use active time for dis
        _sm.StartCoroutine(_sm.RecoveryCollider(0.2f, _smMV.attCollider));
    }
}
