using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDown : StateMachineBehaviour
{
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float downOffset;
    Vector3 aimedPoint;
    Vector3 previousPosition;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aimedPoint = new Vector3(previousPosition.x, previousPosition.y - downOffset);
        previousPosition = animator.transform.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("en BossDown");
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
