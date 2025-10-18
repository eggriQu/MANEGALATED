using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbed : Grounded
{
    public Grabbed(MovementSM stateMachine, AttackSM stateMachineATT) : base("Grabbed", stateMachine, stateMachineATT)
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

    private IEnumerator Hit(float time)
    {
        yield return new WaitForSeconds(time);
        if (_sm.hurtController.smMVOpp.character == 0)
        {
            ApplyForce(20, 25, _sm.rbEnemy.velocity.x);
            _sm.hp -= 20 * _sm.damageMultiplier;
        }
        else if (_sm.hurtController.smMVOpp.character == 1)
        {
            ApplyForce(25, 37.5f, _sm.rbEnemy.velocity.x);
            _sm.hurtController.smMV.hp -= 35 * _sm.damageMultiplier;
        }
        _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.airHit, 0.03333333333f));
    }

    public override void Enter()
    {
        base.Enter();
        _sm.hurtController.hitStop = 0.2f;
        Vector2 velocity = _sm.rb.velocity;
        velocity.x = 0;
        _sm.rb.velocity = velocity;
        if (!_sm.trackOpp.flipped)
        {
            _sm.rb.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
        }
        else
        {
            _sm.rb.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
        }
        if (_sm.character == 0)
        {
            _sm.anim.Play("Shoto Grabbed");
        }
        else if (_sm.character == 1)
        {
            _sm.anim.Play("Tank_Grabbed");
        }
        _smATT.StopAllCoroutines();
        _sm.StopAllCoroutines();
        if (_sm.hurtController.smMVOpp.character == 0)
        {
            _sm.StartCoroutine(Hit(0.45f));
        }
        else if (_sm.hurtController.smMVOpp.character == 1)
        {
            _sm.StartCoroutine(Hit(0.6f));
        }
    }
}
