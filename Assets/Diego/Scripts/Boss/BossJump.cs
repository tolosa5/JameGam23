using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJump : StateMachineBehaviour
{
    [SerializeField] float jumpSpeed = 15f;
    Transform jumpPlace;
    Vector3 aimedPoint;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jumpPlace = GameObject.FindGameObjectWithTag("BossJump").transform;
        aimedPoint = jumpPlace.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, aimedPoint, jumpSpeed * Time.deltaTime);
        if (animator.transform.position == aimedPoint)
        {
            animator.SetTrigger("Run");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Run");
    }
}
