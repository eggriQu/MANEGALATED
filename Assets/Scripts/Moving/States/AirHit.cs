using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirHit : Grounded
{
    public float hitStunAir;

    public AirHit(MovementSM stateMachine, AttackSM stateMachineATT) : base("AirHit", stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    void ApplyForce(float force, float upForce, float force2)
    {
        if (_sm.trackOpp.flipped)
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = force;
            velocity.y = upForce;
            Vector2 enemyVelocity = _sm.rbEnemy.velocity;
            enemyVelocity.x = -force2;
            _sm.rb.velocity = velocity;
            _sm.rbEnemy.velocity = enemyVelocity;
        }
        else
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = -force;
            velocity.y = upForce;
            Vector2 enemyVelocity = _sm.rbEnemy.velocity;
            enemyVelocity.x = force2;
            _sm.rb.velocity = velocity;
            _sm.rbEnemy.velocity = enemyVelocity;
        }
    }

    public override void Enter()
    {
        base.Enter();
        hitStunAir = _sm.hurtController.hitStun;
        if (_sm.character == 0)
        {
            _sm.anim.Play("AirHit");
        }
        else if (_sm.character == 1)
        {
            _sm.anim.Play("Tank_AirHit");
        }
        _smATT.StopAllCoroutines();
        _sm.StopAllCoroutines();
        _smATT.StartCoroutine(_smATT.ChangeState(_smATT.notAttacking, _sm.hurtController.hitStun, 0, 0));
        _smATT.StartCoroutine(_smATT.HitStop(_sm.hurtController.hitStop));

        if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto5P)
        {
            ApplyForce(15, 14, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 5 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto6P)
        {
            ApplyForce(15, 14, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 9 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto2P)
        {
            ApplyForce(11, 14, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 5 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shotoJP)
        {
            ApplyForce(10, _sm.rbEnemy.velocity.y, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 5 * _sm.damageMultiplier;
        }

        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto5K)
        {
            ApplyForce(9, 14, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 11 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto4K)
        {
            ApplyForce(4, 35, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 12 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto2H)
        {
            if (_sm.hurtController.smMVOpp.character == 0)
            {
                ApplyForce(15, 18, _sm.rbEnemy.velocity.x);
                _sm.hurtController.smMV.hp -= 14 * _sm.damageMultiplier;
            }
            else
            {
                ApplyForce(7, 35, _sm.rbEnemy.velocity.x);
                _sm.hurtController.smMV.hp -= 19 * _sm.damageMultiplier;
            }
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shotoJK)
        {
            ApplyForce(13, _sm.rbEnemy.velocity.y, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 12 * _sm.damageMultiplier;
        }

        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto5H)
        {
            ApplyForce(20, 14, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 16 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto6H)
        {
            ApplyForce(20, 14, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 24 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto2K)
        {
            ApplyForce(14, 30, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 12 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shotoJH)
        {
            ApplyForce(16, _sm.rbEnemy.velocity.y, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 9 * _sm.damageMultiplier;
        }

        else if (_sm.hurtController.smATTOpp.currentState == _sm.hurtController.smATTOpp.shoto623H)
        {
            ApplyForce(12, 30, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 29 * _sm.damageMultiplier;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_sm.hurtController.grounded)
        {
            _sm.ChangeState(_sm.knockdown);
            Vector2 velocity = _sm.rb.velocity;
            if (!_sm.trackOpp.flipped)
            {
                velocity.x = -2;
            }
            else
            {
                velocity.x = 2;
            }
            _sm.rb.velocity = velocity;
        }
    }
}
