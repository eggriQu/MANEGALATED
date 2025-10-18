using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockdown : Grounded
{
    private float hitStunAir;

    public Knockdown(MovementSM stateMachine, AttackSM stateMachineATT) : base("Knockdown", stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    public override void Enter()
    {
        base.Enter();
        _smATT.StopAllCoroutines();
        _sm.StopAllCoroutines();
        hitStunAir = _sm.airHit.hitStunAir;
        if (_sm.previousState == _sm.airHit && _sm.previousState2 != _sm.grabbed)
        {
            if (_sm.character == 0)
            {
                _sm.anim.Play("Knockdown");
            }
            else if (_sm.character == 1)
            {
                _sm.anim.Play("Tank_Knockdown");
            }
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, hitStunAir));
            _smATT.StartCoroutine(_smATT.ChangeState(_smATT.notAttacking, hitStunAir, 0, 0));
        }
        else if (_sm.previousState == _sm.airHit && _sm.previousState2 == _sm.grabbed)
        {
            if (_sm.character == 0)
            {
                _sm.anim.Play("HardKnockdown");
            }
            else if (_sm.character == 1)
            {
                _sm.anim.Play("Tank_HardKnockdown");
            }
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.9166666667f));
            _smATT.StartCoroutine(_smATT.ChangeState(_smATT.notAttacking, 0.9166666667f, 0, 0));
        }
        else if (_sm.hp <= 0)
        {
            _sm.hp = 1;
            if (_sm.character == 0)
            {
                _sm.anim.Play("Shoto Die");
            }
            else if (_sm.character == 1)
            {
                _sm.anim.Play("Tank Die");
            }
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = 0;
            _sm.rb.velocity = velocity;
            _sm.hurtController.gameOverScreen.SetActive(true);
            if (_sm.isPlayer1)
            {
                _sm.hurtController.playerWinText.SetText("Player 2 Wins!");
            }
            else
            {
                _sm.hurtController.playerWinText.SetText("Player 1 Wins!");
            }
        }
        else if (_sm.hp > 0 && _sm.hurtController.smMVOpp.hp <= 1)
        {
            if (_sm.character == 0)
            {
                _sm.anim.Play("Neutral");
            }
            else if (_sm.character == 1)
            {
                _sm.anim.Play("Tank_Neutral");
            }
            Vector2 velocity = _sm.rb.velocity;
            velocity.x = 0;
            _sm.rb.velocity = velocity;
        }
        else
        {
            if (_sm.character == 0)
            {
                _sm.anim.Play("HardKnockdown");
            }
            else if (_sm.character == 1)
            {
                _sm.anim.Play("Tank_HardKnockdown");
            }
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.9166666667f));
            _smATT.StartCoroutine(_smATT.ChangeState(_smATT.notAttacking, 0.9166666667f, 0, 0));
            _smATT.StartCoroutine(_smATT.HitStop(_sm.hurtController.hitStop));
        }
    }
}
