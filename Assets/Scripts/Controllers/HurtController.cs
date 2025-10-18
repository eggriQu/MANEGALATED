using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HurtController : MonoBehaviour
{
    public MovementSM smMV;
    public MovementSM smMVOpp;
    public AttackSM smATT;
    public AttackSM smATTOpp;
    public Collider2D enemyCollider;
    public Collider2D enemyProjectileCollider;
    public Collider2D enemyGrabCollider;
    public GameObject[] fireball;
    public GameObject enemyFireball;
    public GameObject gameOverScreen;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI playerWinText;
    public bool grounded;
    public bool hitByProjectile;
    private int _groundLayer = 1 << 6;
    public float horizontalInput;
    public float hitStun;
    public float hitStop;
    public float blockStun;
    public float dashTime;
    public bool hit1;
    public bool hit2;
    public bool hit3;

    public bool lightAttackInput;
    public bool medAttackInput;
    public bool heavyAttackInput;
    public bool notMedInput;
    public bool grabInput;
    public bool attackInput;
    public bool downInput;

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = smMV.rb.velocity.y < Mathf.Epsilon && smMV.rb.IsTouchingLayers(_groundLayer);
        
        if (dashTime < 1)
        {
            dashTime += 0.0166666666667f;
        }
        else
        {
            dashTime = 1;
        }

        if (smMV.hp <= 0)
        {
            smMV.hp = 0;
            smMV.ChangeState(smMV.knockdown);
            smMVOpp.ChangeState(smMVOpp.knockdown);
        }
    }

    void Update()
    {
        if (smMV.isPlayer1)
        {
            lightAttackInput = Input.GetKeyDown(KeyCode.J);
            medAttackInput = Input.GetKeyDown(KeyCode.K);
            heavyAttackInput = Input.GetKeyDown(KeyCode.L);
            grabInput = Input.GetKeyDown(KeyCode.Semicolon);
            downInput = Input.GetKey(KeyCode.S);
            horizontalInput = Input.GetAxisRaw("Horizontal");
            healthText.SetText("Player1: " + smMV.hp);
        }
        else
        {
            lightAttackInput = Input.GetKeyDown(KeyCode.Numlock);
            medAttackInput = Input.GetKeyDown(KeyCode.KeypadDivide);
            heavyAttackInput = Input.GetKeyDown(KeyCode.KeypadMultiply);
            grabInput = Input.GetKeyDown(KeyCode.KeypadPlus);
            downInput = Input.GetKey(KeyCode.DownArrow);
            horizontalInput = Input.GetAxisRaw("HorizontalP2");
            healthText.SetText("Player2: " + smMV.hp);
        }
        attackInput = lightAttackInput || medAttackInput || heavyAttackInput;
        notMedInput = lightAttackInput || heavyAttackInput;

        if (smMV.currentState != smMV.hurt1MV)
        {
            enemyProjectileCollider = null;
            enemyFireball = null;
        }

        if (smMV.isPlayer1)
        {
            fireball[0] = GameObject.Find("ShotoProjectile(Clone)");
            enemyProjectileCollider = GameObject.Find("ShotoProjectileP2(Clone)").GetComponent<BoxCollider2D>();
            enemyFireball = GameObject.Find("ShotoProjectileP2(Clone)");
        }
        else
        {
            fireball[0] = GameObject.Find("ShotoProjectileP2(Clone)");
            enemyProjectileCollider = GameObject.Find("ShotoProjectile(Clone)").GetComponent<BoxCollider2D>();
            enemyFireball = GameObject.Find("ShotoProjectile(Clone)");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == enemyCollider || collision == enemyProjectileCollider)
        {
            if (smMV.currentState != smMV.blockingState && grounded)
            {
                if (!hit1 && !hit2 && !hit3)
                {
                    if (collision == enemyProjectileCollider)
                    {
                        Destroy(enemyFireball);
                        hitByProjectile = true;
                        CalculateHitStun();
                        smMV.ChangeState(smMV.hurtSpecial);
                    }
                    else
                    {
                        hitByProjectile = false;
                        hit1 = true;
                        hit2 = false;
                        hit3 = false;
                        CalculateHitStun();
                        smMV.ChangeState(smMV.hurt1MV);
                    }
                }
                else if (hit1 && hitStun > 0)
                {
                    Destroy(enemyFireball);
                    hit1 = false;
                    hit2 = true;
                    hit3 = false;
                    CalculateHitStun();
                    smMV.ChangeState(smMV.hurt1MV);
                }
                else if (hit2 && hitStun > 0)
                {
                    Destroy(enemyFireball);
                    hit1 = false;
                    hit2 = false;
                    hit3 = true;
                    CalculateHitStun();
                    smMV.ChangeState(smMV.hurt1MV);
                }
            }
            else if (smMV.currentState == smMV.blockingState && grounded || smMV.currentState == smMV.blockHit && grounded)
            {
                if (!hit1 && !hit2 && !hit3)
                {
                    hit1 = true;
                    hit2 = false;
                    hit3 = false;
                    CalculateHitStun();
                    smMV.ChangeState(smMV.blockHit);
                }
                else if (collision == enemyCollider && hit1 && hitStun > 0)
                {
                    hit1 = false;
                    hit2 = true;
                    hit3 = false;
                    CalculateHitStun();
                    smMV.ChangeState(smMV.blockHit);
                }
                else if (collision == enemyCollider && hit2 && hitStun > 0)
                {
                    hit1 = false;
                    hit2 = false;
                    hit3 = true;
                    CalculateHitStun();
                    smMV.ChangeState(smMV.blockHit);
                }
            }
            else if (!grounded)
            {
                if (!hit1 && !hit2 && !hit3)
                {
                    if (collision == enemyProjectileCollider)
                    {
                        Destroy(enemyFireball);
                        hitByProjectile = true;
                        CalculateHitStun();
                        smMV.ChangeState(smMV.hurtSpecial);
                    }
                    else
                    {
                        hitByProjectile = false;
                        hit1 = true;
                        hit2 = false;
                        hit3 = false;
                        CalculateHitStun();
                        smMV.ChangeState(smMV.airHit);
                    }
                }
                else if (hit1)
                {
                    hit1 = false;
                    hit2 = true;
                    hit3 = false;
                    CalculateHitStun();
                    smMV.ChangeState(smMV.airHit);
                }
                else if (hit2)
                {
                    hit1 = false;
                    hit2 = false;
                    hit3 = true;
                    CalculateHitStun();
                    smMV.ChangeState(smMV.airHit);
                }
            }
        }
    }

    void CalculateHitStun()
    {
        if (smATTOpp.attackLevel == 0)
        {
            hitStun = 0.2f;
            blockStun = hitStun - 0.05f;
            hitStop = 0.1833333333f;
        }
        else if (smATTOpp.attackLevel == 1)
        {
            hitStun = 0.2333333333f;
            blockStun = hitStun - 0.05f;
            hitStop = 0.2f;
        }
        else if (smATTOpp.attackLevel == 2)
        {
            hitStun = 0.2666666667f;
            blockStun = hitStun - 0.05f;
            hitStop = 0.2166666667f;
        }
        else if (smATTOpp.attackLevel == 3)
        {
            hitStun = 0.3166666667f;
            blockStun = hitStun - 0.05f;
            hitStop = 0.2333333333f;
        }
        else if (smATTOpp.attackLevel == 4)
        {
            hitStun = 0.35f;
            blockStun = hitStun - 0.05f;
            hitStop = 0.25f;
        }
        else
        {
            hitStun = 0.2666666667f;
            blockStun = hitStun - 0.05f;
            hitStop = 0.2166666667f;
        }
    }
}
