 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotAttacking : BaseStateATT
{
    private bool lightAttackInput;
    private bool medAttackInput;
    private bool heavyAttackInput;
    private bool grabInput;
    public bool attackInput;

    private bool grounded;
    private string characterName;

    private int _groundLayer = 1 << 6;

    public NotAttacking(AttackSM stateMachine, MovementSM stateMachineMV) : base("NotAttacking", stateMachine, stateMachineMV)
    {
        _sm = (AttackSM)stateMachine;
        _smMV = (MovementSM)stateMachineMV;
    }

    public override void Enter()
    {
        base.Enter();
        lightAttackInput = false;
        medAttackInput = false;
        _sm.attackLevel = -1;
        if (_smMV.character == 0)
        {
            characterName = "Shoto ";
        }
        else if (_smMV.character == 1)
        {
            characterName = "Tank ";
        }

        Vector2 offset = _sm.damageCollider.offset;
        offset.y = _sm.offsetTing;

        _sm.damageCollider.offset = offset;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        lightAttackInput = _smMV.hurtController.lightAttackInput;
        medAttackInput = _smMV.hurtController.medAttackInput;
        heavyAttackInput = _smMV.hurtController.heavyAttackInput;
        grabInput = _smMV.hurtController.grabInput;
        attackInput = lightAttackInput || medAttackInput || heavyAttackInput || grabInput;
        grounded = _sm.rb.velocity.y < Mathf.Epsilon && _sm.rb.IsTouchingLayers(_groundLayer);

        // Light Attacks
        if (lightAttackInput == true && _smMV.currentState == _smMV.neutralState && grounded ||
            lightAttackInput == true && _smMV.currentState == _smMV.blockingState && grounded ||
            lightAttackInput == true && _smMV.currentState == _smMV.dashState && _smMV.previousState == _smMV.neutralState && grounded)
        {
            // 7 frame startup, 4 Frame active
            // TANK: 7 frame startup, 7 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shoto5P, 0.1166666667f, 0.06666666667f, characterName + "5P");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shoto5P, 0.1166666667f, 0.1166666667f, characterName + "5P");
            }
        }
        else if (lightAttackInput == true && _smMV.currentState == _smMV.movingState && grounded ||
                lightAttackInput == true && _smMV.currentState == _smMV.dashState && _smMV.previousState == _smMV.movingState && grounded)
        {
            // 9 Frame startup, 5 Frame active
            // TANK: 16 Frame startup, 9 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shoto6P, 0.15f, 0.08333333333f, characterName + "6P");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shoto6P, 0.2666666667f, 0.15f, characterName + "6P");
            }
        }
        else if (lightAttackInput == true && _smMV.currentState == _smMV.jumpingState)
        {
            // 5 Frame startup, 3 Frame active
            // TANK: 6 Frame startup, 5 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shotoJP, 0.08333333333f, 0.05f, characterName + "JP");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shotoJP, 0.1f, 0.08333333333f, characterName + "JP");
            }
        }
        else if (lightAttackInput == true && _smMV.currentState == _smMV.crouchState)
        {
            // 5 Frame startup, 4 Frame active
            // TANK: 8 Frame startup, 4 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shoto2P, 0.08333333333f, 0.06666666667f, characterName + "2P");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shoto2P, 0.1333333333f, 0.06666666667f, characterName + "2P");
            }
        }

        // Medium Attacks
        if (medAttackInput == true && _smMV.currentState == _smMV.neutralState && grounded || medAttackInput == true && _smMV.currentState == _smMV.movingState && grounded)
        {
            // 9 frame startup, 5 Frame active
            // TANK: 10 frame startup, 9 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shoto5K, 0.15f, 0.08333333333f, characterName + "5K");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shoto5K, 0.1666666667f, 0.15f, characterName + "5K");
            }
        }
        else if (medAttackInput == true && _smMV.currentState == _smMV.blockingState)
        {
            // 16 frame startup, 6 Frame active
            // TANK: 26 frame startup, 10 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shoto4K, 0.2666666667f, 0.1f, characterName + "4K");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shoto4K, 0.4333333333f, 0.1666666667f, characterName + "4K");
            }
        }
        else if (medAttackInput == true && _smMV.currentState == _smMV.jumpingState)
        {
            // 10 frame startup, 4 frame active
            // TANK: 10 frame startup, 5 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shotoJK, 0.1666666667f, 0.06666666667f, characterName + "JK");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shotoJK, 0.1666666667f, 0.08333333333f, characterName + "JK");
            }
        }
        else if (medAttackInput == true && _smMV.currentState == _smMV.crouchState)
        {
            // 10 frame startup, 5 Frame active
            // TANK: 10 frame startup, 5 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shoto2K, 0.1666666667f, 0.06666666667f, characterName + "2K");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shoto2K, 0.1666666667f, 0.06666666667f, characterName + "2K");
            }
        }

        // Heavy Attacks
        if (heavyAttackInput == true && _smMV.currentState == _smMV.neutralState && grounded ||
            heavyAttackInput == true && _smMV.currentState == _smMV.blockingState && grounded)
        {
            // 12 frame startup, 5 Frame active
            // TANK: 19 frame startup, 5 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shoto5H, 0.2f, 0.08333333333f, characterName + "5H");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shoto5H, 0.3166666667f, 0.08333333333f, characterName + "5H");
            }
        }
        else if (heavyAttackInput == true && _smMV.currentState == _smMV.movingState && grounded 
                && !(_sm.inputController.key2nd == 6 && _sm.inputController.key1st == 2 && _sm.inputController.lastKey == 3 && heavyAttackInput == true && grounded))
        {
            // 15 frame startup, 5 frame active
            // TANK: 26 frame startup, 6 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shoto6H, 0.25f, 0.08333333333f, characterName + "6H");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shoto6H, 0.4333333333f, 0.1f, characterName + "6H");
            }
        }
        else if (heavyAttackInput == true && _smMV.currentState == _smMV.jumpingState)
        {
            // 10 frame startup, 6 frame active
            // TANK:  10 frame startup, 7 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shotoJH, 0.1666666667f, 0.1f, characterName + "JH");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shotoJH, 0.1666666667f, 0.1166666667f, characterName + "JH");
            }
        }
        else if (heavyAttackInput == true && _smMV.currentState == _smMV.crouchState 
                && !(_sm.inputController.key2nd == 6 && _sm.inputController.key1st == 2 && _sm.inputController.lastKey == 3 && heavyAttackInput == true && grounded))
        {
            // 13 frame startup, 3 Frame active
            // TANK: 15 frame startup, 5 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shoto2H, 0.2166666667f, 0.05f, characterName + "2H");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shoto2H, 0.25f, 0.08333333333f, characterName + "2H");
            }
        }

        // Grab
        if (grabInput == true && _smMV.currentState == _smMV.neutralState && grounded ||
            grabInput == true && _smMV.currentState == _smMV.blockingState && grounded ||
            grabInput == true && _smMV.currentState == _smMV.movingState && grounded)
        {
            // 3 frame startup, 3 Frame active
            // TANK: 4 frame startup, 4 frame active
            if (_smMV.character == 0)
            {
                Attack(_sm.shotoGrab, 0.05f, 0.05f, characterName + "Grab");
            }
            else if (_smMV.character == 1)
            {
                Attack(_sm.shotoGrab, 0.06666666667f, 0.06666666667f, characterName + "Grab");
            }
        }

        // Special Attacks
        if (_sm.inputController.timeThreshold < 0.4f)
        {
            // 236P
            // SHOTO: 11 frame startup, 9 frame active
            if (_sm.inputController.key2nd == 2 && _sm.inputController.key1st == 3 && _sm.inputController.lastKey == 6 && lightAttackInput == true && grounded)
            {
                if (_smMV.character == 0)
                {
                    Attack(_sm.shoto236P, 0.1833333333f, 0.15f, characterName + "236P");
                }
                else if (_smMV.character == 1)
                {
                    Attack(_sm.shoto236P, 0.4333333333f, 0.1f, characterName + "236H");
                }
                
            }
            // 623H
            // SHOTO: 8 frame startup, 12 frame active
            else if (_sm.inputController.key2nd == 6 && _sm.inputController.key1st == 2 && _sm.inputController.lastKey == 3 && heavyAttackInput == true && grounded && _smMV.character == 0)
            {
                Attack(_sm.shoto623H, 0.1333333333f, 0.2f, "Shoto 623H");
            }
        }
    }

    private void Attack(BaseStateATT state, float startup, float active, string animationName)
    {
        _sm.ChangeToStartup(_sm.startup);
        _smMV.ChangeState(_smMV.startupMV);
        _sm.StartCoroutine(_sm.ChangeState(state, startup, active, 0));
        _sm.anim.Play(animationName);
    }
}
