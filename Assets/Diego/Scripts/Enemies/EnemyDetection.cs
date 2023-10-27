using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] float viewRadius;
    [SerializeField] float viewAngle;

    [SerializeField] LayerMask isPlayer, isObstacle;
    Transform player;
    Vector3 playerTarget;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerTarget = (transform.position - player.position).normalized;
    }

    public void OnPlayerDetection()
    {
        if(Physics2D.Raycast(transform.position, transform.right, viewRadius, isObstacle) == false)
        {
            if(Physics2D.Raycast(transform.position, transform.right, viewRadius, isPlayer))
            {
                Debug.Log("visto");
                EnemyStateMachine enemyStateScr = GetComponent<EnemyStateMachine>();
                enemyStateScr.SwitchState(0);
            }
        }
        
    }

    private void OnDrawGizmos() 
    {
        //Debug.DrawRay(transform.position, transform.right, Color.red, viewRadius);
    }
}
