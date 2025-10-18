using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    private MovementSM smMV;
    private AttackSM smATT;
    private AttackSM smATTOpp;
    private Collider2D enemyGrabCollider;
    public bool grabHit;
    private bool grounded;
    private int _groundLayer = 1 << 6;

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = smMV.rb.velocity.y < Mathf.Epsilon && smMV.rb.IsTouchingLayers(_groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (grounded)
        {
            if (collision == enemyGrabCollider && smMV.currentState != smMV.knockdown)
            {
                smMV.ChangeState(smMV.grabbed);
                smATTOpp.StartCoroutine(smATTOpp.ChangeState(smATTOpp.shotoGrabHit, 0, 0, 0));
                smATTOpp.anim.StopPlayback();
                grabHit = true;
            }
            else
            {
                grabHit = false;
            }
        }
    }
}