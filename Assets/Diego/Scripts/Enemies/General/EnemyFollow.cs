using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    Transform player;
    [HideInInspector] public bool isFlipped = false;

    [SerializeField] int speed = 3;
    [SerializeField] int attackRange = 3;
    [SerializeField] float feetRadius = 0.5f;

    [SerializeField] Transform feet;
    [SerializeField] LayerMask isGround, isPlayer;
    [SerializeField] bool isGrounded;

    Animator anim;
    Rigidbody2D rb;
    EnemyStateMachine stateMachine;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine = GetComponent<EnemyStateMachine>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void FollowPlayer()
    {
        if (stateMachine.currentState == EnemyStateMachine.States.Following)
        {
            IsGrounded();
            Debug.Log(IsGrounded());
            LookAtPlayer();
            if (isGrounded && Vector3.Distance(transform.position, player.position) >= attackRange)
            {
                Vector3 target = new Vector3(player.position.x, rb.position.y);
                Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                anim.SetTrigger("Attack");
            }
        }
    }

    public void DealDamage()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position + transform.right, attackRange, isPlayer);
        if (coll != null)
        {
            coll.GetComponent<Player>().GetHit(1);
        }
    }

    public bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, isGround);
        
        return isGrounded;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(feet.position, feetRadius);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isFlipped = true;
        }
    }
}
