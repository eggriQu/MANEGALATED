using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAttack : Grounded
{
    public InAttack(MovementSM stateMachine, AttackSM stateMachineATT) : base("InAttack", stateMachine, stateMachineATT)
    {
        _sm = (MovementSM)stateMachine;
        _smATT = (AttackSM)stateMachineATT;
    }

    public override void Enter()
    {
        base.Enter();

        // Float is always recovery of attack
        // Light Attacks
        if (_smATT.currentState == _smATT.shoto5P && grounded)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.1166666667f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.1333333333f));
            }
        }
        else if (_smATT.currentState == _smATT.shoto6P && grounded)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.3f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.25f));
            }
        }
        else if (_smATT.currentState == _smATT.shotoJP)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.jumpingState, 0.1333333333f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.jumpingState, 0.1333333333f));
            }
        }
        else if (_smATT.currentState == _smATT.shoto2P)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.crouchState, 0.1333333333f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.crouchState, 0.15f));
            }
        }

        // Medium Attacks
        else if (_smATT.currentState == _smATT.shoto5K && grounded)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.2666666667f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.2f));
            }
        }
        else if (_smATT.currentState == _smATT.shoto4K && grounded)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.2166666667f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.1833333333f));
            }
        }
        else if (_smATT.currentState == _smATT.shotoJK)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.jumpingState, 0.1333333333f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.jumpingState, 0.2333333333f));
            }
        }
        else if (_smATT.currentState == _smATT.shoto2K)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.crouchState, 0.1666666667f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.crouchState, 0.2166666667f));
            }
        }

        // Heavy Attacks
        else if (_smATT.currentState == _smATT.shoto5H && grounded)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.3666666667f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.35f));
            }
        }
        else if (_smATT.currentState == _smATT.shoto6H && grounded)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.3666666667f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.3333333333f));
            }
        }
        else if (_smATT.currentState == _smATT.shotoJH)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.jumpingState, 0.3166666667f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.jumpingState, 0.2666666667f));
            }
        }
        else if (_smATT.currentState == _smATT.shoto2H)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.crouchState, 0.2f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.crouchState, 0.2833333333f));
            }
        }

        // Grab
        else if (_smATT.currentState == _smATT.shotoGrab && grounded)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.6666666667f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.6f));
            }
        }
        else if (_smATT.currentState == _smATT.shotoGrabHit && grounded)
        {
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 1.583333333f));
        }

        //Special Attacks
        else if (_smATT.currentState == _smATT.shoto236P && grounded)
        {
            if (_sm.character == 0)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.35f));
            }
            else if (_sm.character == 1)
            {
                _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.3333333333f));
            }
        }
        else if (_smATT.currentState == _smATT.shoto623H && grounded)
        {
            _sm.StartCoroutine(_sm.ChangeFromAttack(_sm.neutralState, 0.2666666667f));
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_smATT.currentState == _smATT.notAttacking && grounded)
        {
            _sm.ChangeState(_sm.neutralState);
        }
    }
}
