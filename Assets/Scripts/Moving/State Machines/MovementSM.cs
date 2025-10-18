using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    [HideInInspector]
    public Neutral neutralState;
    [HideInInspector]
    public Moving movingState;
    [HideInInspector]
    public Blocking blockingState;
    [HideInInspector]
    public Jumping jumpingState;
    [HideInInspector]
    public Crouching crouchState;
    [HideInInspector]
    public Dash dashState;
    [HideInInspector]
    public InAttack inAttack;
    [HideInInspector]
    public StartupMV startupMV;
    [HideInInspector]
    public HurtMV hurt1MV;
    [HideInInspector]
    public HurtSpecial hurtSpecial;
    [HideInInspector]
    public Knockdown knockdown;
    [HideInInspector]
    public BlockHit blockHit;
    [HideInInspector]
    public AirHit airHit;
    [HideInInspector]
    public Grabbed grabbed;

    public AttackSM attackSM;
    public Rigidbody2D rb;
    public Transform playerTransform;
    public Rigidbody2D rbEnemy;
    public Animator anim;
    public BoxCollider2D attCollider;
    public BoxCollider2D mvCollider;
    public BoxCollider2D grabCollider;
    public TrackOpponentsPosition trackOpp;
    public HurtController hurtController;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float jumpForce;
    public int character;
    public bool isPlayer1;
    [HideInInspector]
    public float forceMultiplier;
    public float hp;
    public float damageMultiplier;

    private void Awake()
    {
        neutralState = new Neutral(this, attackSM);
        movingState = new Moving(this, attackSM);
        blockingState = new Blocking(this, attackSM);
        jumpingState = new Jumping(this, attackSM);
        crouchState = new Crouching(this, attackSM);
        dashState = new Dash(this, attackSM);
        inAttack = new InAttack(this, attackSM);
        startupMV = new StartupMV(this, attackSM);
        hurt1MV = new HurtMV(this, attackSM);
        hurtSpecial = new HurtSpecial(this, attackSM);
        blockHit = new BlockHit(this, attackSM);
        airHit = new AirHit(this, attackSM);
        grabbed = new Grabbed(this, attackSM);
        knockdown = new Knockdown(this, attackSM);

        switch (character)
        {
            case 0:
                speed = 8.8f;
                jumpForce = 32;
                forceMultiplier = 1;
                hp = 150;
                damageMultiplier = 1.12f;
                break;
            case 1:
                speed = 6.5f;
                jumpForce = 28;
                forceMultiplier = 1.2f;
                hp = 200;
                damageMultiplier = 1f;
                break;
            default:
                break;
        }
    }

    protected override BaseState GetInitialState()
    {
        return neutralState;
    }
}
