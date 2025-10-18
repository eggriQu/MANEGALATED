using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : Grounded
{
    public BlockHit(MovementSM stateMachine, AttackSM stateMachineATT) : base("BlockHit", stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    void ApplyForce(float force, float force2)
    {
        if (_sm.trackOpp.flipped)
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = force;
            Vector2 enemyVelocity = _sm.rbEnemy.velocity;
            enemyVelocity.x = -force2;
            _sm.rb.velocity = velocity;
            _sm.rbEnemy.velocity = enemyVelocity;
        }
        else
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = -force;
            Vector2 enemyVelocity = _sm.rbEnemy.velocity;
            enemyVelocity.x = force2;
            _sm.rb.velocity = velocity;
            _sm.rbEnemy.velocity = enemyVelocity;
        }
    }

    public override void Enter()
    {
        base.Enter();
        if (_sm.character == 0)
        {
            _sm.anim.Play("BlockHit");
        }
        else if (_sm.character == 1)
        {
            _sm.anim.Play("Tank_BlockHit");
        }
        _smATT.StopAllCoroutines();
        _sm.StopAllCoroutines();
        _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, _sm.hurtController.blockStun));
        _smATT.StartCoroutine(_smATT.ChangeState(_smATT.notAttacking, _sm.hurtController.blockStun, 0, 0));
        _smATT.StartCoroutine(_smATT.HitStop(_sm.hurtController.hitStop));

        // light attacks
        if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto5P || _sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto2P ||
        _sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shotoJP)
        {
            ApplyForce(6.5f * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 1.2f * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto6P)
        {
            ApplyForce(10, _sm.rbEnemy.velocity.x);
            _sm.hp -= 5 * _sm.damageMultiplier;
        }

        // Medium attacks
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto5K || _sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto2K
        || _sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shotoJK)
        {
            ApplyForce(2.5f * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 6 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto4K)
        {
            ApplyForce(3.5f * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 7 * _sm.damageMultiplier;
        }

        // Heavy attacks
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto5H || _sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto6H
        || _sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto623H)
        {
            ApplyForce(9, _sm.rbEnemy.velocity.x);
            _sm.hp -= 14 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto2H)
        {
            if (_sm.hurtController.smMVOpp.character == 0)
            {
                ApplyForce(1.5f * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
                _sm.ChangeState(_sm.knockdown);
                _sm.hp -= 12 * _sm.damageMultiplier;
            }
            else
            {
                _sm.hp -= 10 * _sm.damageMultiplier;
            }
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shotoJH)
        {
            ApplyForce(3 * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 10 * _sm.damageMultiplier;
        }
    }
}
