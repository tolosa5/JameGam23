using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    [SerializeField] float speed = 2.5f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float feetRadius;
    [SerializeField] LayerMask isGround;

    bool isGrounded;

    Transform feet;
    Transform player;
    Rigidbody2D rb;
    Boss boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        feet = animator.transform.GetChild(0);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        
        if (IsGrounded())
        {
            Vector3 target = new Vector3(player.position.x, rb.position.y);
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        
        if (Vector3.Distance(rb.position, player.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
            Debug.Log("attackBoss");
        }
    }

    bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, isGround);
        return isGrounded;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
