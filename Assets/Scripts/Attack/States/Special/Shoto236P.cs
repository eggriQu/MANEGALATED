using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoto236P : BaseStateATT
{
    public Shoto236P(AttackSM stateMachine, MovementSM stateMachineMV) : base("Shoto236P", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        if (_smMV.character == 0)
        {
            // SHOTO
            // 18 Frame recovery
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.35f));
            _smMV.ChangeState(_smMV.inAttack);
            _sm.attackLevel = 4;

            if (_smMV.hurtController.fireball[0] == null)
            {
                if (!_sm.trackOpp.flipped)
                {
                    _sm.Spawn(_sm.projectile236P, new Vector3(_sm.playerTransform.position.x, (_sm.playerTransform.position.y - 0.5f), _sm.playerTransform.position.z), new Quaternion(0 ,0, 0, 0));
                }
                else if (_sm.trackOpp.flipped)
                {
                    _sm.Spawn(_sm.projectile236P, new Vector3(_sm.playerTransform.position.x, (_sm.playerTransform.position.y - 0.5f), _sm.playerTransform.position.z), new Quaternion(0 ,0, -180, 0));
                }
            }
        }
        else
        {
            // TANK : 20 frame recovery, 6 frame active
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0.3333333333f));
            _sm.StartCoroutine(_sm.RecoveryCollider(0.1f, _smMV.attCollider));
            _smMV.ChangeState(_smMV.inAttack);

            Vector2 size = _smMV.attCollider.size;
            Vector2 offset = _smMV.attCollider.offset;
            Vector2 mvSize = _sm.mvCollider.size;
            Vector2 mvOffset = _sm.mvCollider.offset;

            mvSize.x = 0.64876f;
            mvSize.y = 0.5648946f;
            size.x = 1.84995f;
            size.y = 0.1623514f;
            if (!_sm.trackOpp.flipped)
            {
                mvOffset.x = 0.4370845f;
                offset.x = 1.430082f;
            }
            else
            {
                mvOffset.x = -0.4370845f;
                offset.x = -1.430082f;
            }
            mvOffset.y = -0.212537f;
            offset.y = -0.4184458f;


            _smMV.attCollider.size = size;
            _smMV.attCollider.offset = offset;

            _sm.attackLevel = 4;
            _smMV.mvCollider.size = mvSize;
            _smMV.mvCollider.offset = mvOffset;
        }

        if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV)
        {

        }
        else
        {
            Vector2 velocity = _smMV.rb.velocity;
            velocity.x = 0;
            _smMV.rb.velocity = velocity;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV && _smMV.hurtController.attackInput
            && !_smMV.hurtController.smMVOpp.hurtController.hit3 && !_smMV.hurtController.downInput)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.neutralState);
        }
        else if (_smMV.hurtController.smMVOpp.currentState == _smMV.hurtController.smMVOpp.hurt1MV && _smMV.hurtController.attackInput
            && !_smMV.hurtController.smMVOpp.hurtController.hit3 && _smMV.hurtController.downInput)
        {
            _sm.StopAllCoroutines();
            _smMV.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeState(_sm.notAttacking, 0, 0, 0));
            _smMV.ChangeState(_smMV.crouchState);
        }
    }
}
