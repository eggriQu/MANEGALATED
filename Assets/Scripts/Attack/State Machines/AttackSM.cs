using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSM : StateMachineATT
{
    [HideInInspector]
    public NotAttacking notAttacking;
    [HideInInspector]
    public Shoto5P shoto5P;
    [HideInInspector]
    public Shoto6P shoto6P;
    [HideInInspector]
    public ShotoJP shotoJP;
    [HideInInspector]
    public Shoto2P shoto2P;
    [HideInInspector]
    public Shoto5K shoto5K;
    [HideInInspector]
    public Shoto4K shoto4K;
    [HideInInspector]
    public ShotoJK shotoJK;
    [HideInInspector]
    public Shoto2H shoto2H;
    [HideInInspector]
    public Shoto5H shoto5H;
    [HideInInspector]
    public Shoto6H shoto6H;
    [HideInInspector]
    public ShotoJH shotoJH;
    [HideInInspector]
    public Shoto2K shoto2K;
    [HideInInspector]
    public Shoto236P shoto236P;
    [HideInInspector]
    public Shoto623H shoto623H;
    [HideInInspector]
    public ShotoGrab shotoGrab;
    [HideInInspector]
    public ShotoGrabHit shotoGrabHit;
    [HideInInspector]
    public Startup startup;

    public MovementSM movementSM;
    public Rigidbody2D rb;
    public Rigidbody2D rbEnemy;
    public Animator anim;
    public BoxCollider2D damageCollider;
    public BoxCollider2D mvCollider;
    public BoxCollider2D grabCollider;
    public TrackOpponentsPosition trackOpp;
    public GrabController grabController;
    public InputController inputController;
    public GameObject projectile236P;
    public Transform playerTransform;
    public int attackLevel;
    public bool isPlayer1;
    public float offsetTing;

    private void Awake()
    {
        notAttacking = new NotAttacking(this, movementSM);
        shoto5P = new Shoto5P(this, movementSM);
        shoto6P = new Shoto6P(this, movementSM);
        shotoJP = new ShotoJP(this, movementSM);
        shoto2P = new Shoto2P(this, movementSM);
        shoto5K = new Shoto5K(this, movementSM);
        shoto4K = new Shoto4K(this, movementSM);
        shotoJK = new ShotoJK(this, movementSM);
        shoto2K = new Shoto2K(this, movementSM);
        shoto5H = new Shoto5H(this, movementSM);
        shoto6H = new Shoto6H(this, movementSM);
        shotoJH = new ShotoJH(this, movementSM);
        shoto2H = new Shoto2H(this, movementSM);
        shoto236P = new Shoto236P(this, movementSM);
        shoto623H = new Shoto623H(this, movementSM);
        shotoGrab = new ShotoGrab(this, movementSM);
        shotoGrabHit = new ShotoGrabHit(this, movementSM);
        startup = new Startup(this, movementSM);
    }

    protected override BaseStateATT GetInitialState()
    {
        return notAttacking;
    }
}
