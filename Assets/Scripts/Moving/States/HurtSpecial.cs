using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtSpecial : Grounded
{
    public HurtSpecial(MovementSM stateMachine, AttackSM stateMachineATT) : base("HurtSpecial", stateMachine, stateMachineATT)
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
        }
        else
        {
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = -force;
            Vector2 enemyVelocity = _sm.rbEnemy.velocity;
            enemyVelocity.x = force2;
            _sm.rb.velocity = velocity;
        }
    }

    void ApplyAirForce(float force, float upForce, float force2)
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
        _smATT.StopAllCoroutines();
        _sm.StopAllCoroutines();
        if (_sm.hurtController.grounded && _sm.currentState != _sm.blockingState)
        {
            if (_sm.character == 0)
            {
                _sm.anim.Play("Hurt");
            }
            else if (_sm.character == 1)
            {
                _sm.anim.Play("Tank_Hit");
            }
            ApplyForce(8.5f, _sm.rbEnemy.velocity.x);
            _sm.hp -= 17 * _sm.damageMultiplier;
            _smATT.StopAllCoroutines();
            _sm.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, _sm.hurtController.hitStun));
            _smATT.StartCoroutine(_smATT.ChangeState(_smATT.notAttacking, _sm.hurtController.hitStun, 0, 0));
            _smATT.StartCoroutine(_smATT.HitStop(_sm.hurtController.hitStop));
        }
        else if (_sm.hurtController.grounded && _sm.currentState == _sm.blockingState)
        {
            if (_sm.character == 0)
            {
                _sm.anim.Play("BlockHit");
            }
            else if (_sm.character == 1)
            {
                _sm.anim.Play("Tank_BlockHit");
            }
            ApplyForce(4.25f, _sm.rbEnemy.velocity.x);
            _smATT.StopAllCoroutines();
            _sm.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, _sm.hurtController.hitStun/2));
            _smATT.StartCoroutine(_smATT.ChangeState(_smATT.notAttacking, _sm.hurtController.hitStun/2, 0, 0));
            _smATT.StartCoroutine(_smATT.HitStop(_sm.hurtController.hitStop/2));
        }
        else if (!_sm.hurtController.grounded)
        {
            if (_sm.character == 0)
            {
                _sm.anim.Play("AirHit");
            }
            else if (_sm.character == 1)
            {
                _sm.anim.Play("Tank_AirHit");
            }
            ApplyAirForce(14f, 12f, _sm.rbEnemy.velocity.x);
            _sm.hp -= 17 * _sm.damageMultiplier;
            _smATT.StopAllCoroutines();
            _sm.StopAllCoroutines();
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.airHit, 0));
        }
    }
}
