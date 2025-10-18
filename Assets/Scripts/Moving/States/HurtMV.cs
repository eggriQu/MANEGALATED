using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtMV : Grounded
{
    public HurtMV(MovementSM stateMachine, AttackSM stateMachineATT) : base("HurtMV", stateMachine, stateMachineATT)
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
            if (_sm.hurtController.hit1)
            {
                _sm.anim.Play("Hurt");
            }
            else if (_sm.hurtController.hit2)
            {
                _sm.anim.Play("Hurt2");
            }
            else if (_sm.hurtController.hit3)
            {
                _sm.anim.Play("Hurt");
            }
        }
        else if (_sm.character == 1)
        {
            if (_sm.hurtController.hit1)
            {
                _sm.anim.Play("Tank_Hit");
            }
            else if (_sm.hurtController.hit2)
            {
                _sm.anim.Play("Tank_Hit2");
            }
            else if (_sm.hurtController.hit3)
            {
                _sm.anim.Play("Tank_Hit");
            }
        }
        _smATT.StopAllCoroutines();
        _sm.StopAllCoroutines();
        _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, _sm.hurtController.hitStun));
        _smATT.StartCoroutine(_smATT.ChangeState(_smATT.notAttacking, _sm.hurtController.hitStun, 0, 0));
        _smATT.StartCoroutine(_smATT.HitStop(_sm.hurtController.hitStop));

        //Light attacks
        if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto5P || _sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto2P ||
        _sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shotoJP)
        {
            ApplyForce(8.5f * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 5 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto6P)
        {
            ApplyForce(15 * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 9 * _sm.damageMultiplier;
        }

        //Medium attacks
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto5K || _sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto2K)
        {
            ApplyForce(2.5f * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 11 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto4K)
        {
            if (_sm.hurtController.smMVOpp.character == 0)
            {
                _sm.hurtController.grounded = false;
                _sm.ChangeState(_sm.airHit);
                _sm.hp -= 5 * _sm.damageMultiplier;
            }
            else
            {
                ApplyForce(14 * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
                _sm.hp -= 15 * _sm.damageMultiplier;
            }
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shotoJK)
        {
            ApplyForce(5 * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 9 * _sm.damageMultiplier;
        }

        //Hard attacks
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto5H)
        {
            ApplyForce(14 * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 16 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto6H)
        {
            ApplyForce(17 * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 24 * _sm.damageMultiplier;
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
                _sm.hurtController.grounded = false;
                _sm.ChangeState(_sm.airHit);
                _sm.hp -= 13 * _sm.damageMultiplier;
            }
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shotoJH)
        {
            ApplyForce(5 * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.hp -= 9 * _sm.damageMultiplier;
        }

        //Special attacks
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto236P)
        {
            ApplyForce(1.5f * _sm.hurtController.smMVOpp.forceMultiplier, _sm.rbEnemy.velocity.x);
            _sm.ChangeState(_sm.knockdown);
            _sm.hp -= 15 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto623H)
        {
            _sm.hurtController.grounded = false;
            _sm.ChangeState(_sm.airHit);
            _sm.hp -= 21 * _sm.damageMultiplier;
        }
    }
}
