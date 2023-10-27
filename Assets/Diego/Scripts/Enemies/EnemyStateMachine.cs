using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    EnemyDetection enemyDetectionScr;
    EnemyFollow enemyFollowScr;
    PatrolAI patrolScr;

    [HideInInspector] public enum States{Patrolling, Following, Leaving};
    [HideInInspector] public States currentState;

    private void Start() 
    {
        enemyDetectionScr = GetComponent<EnemyDetection>();
        enemyFollowScr = GetComponent<EnemyFollow>();
    }
    void Update()
    {
        switch (currentState)
        {
            default:
            case States.Patrolling:
            //mirar si esta en su campo de vision
            Debug.Log("detectando");
            enemyDetectionScr.OnPlayerDetection();
            //patrullar por los puntos que le diga
            //patrolScr.PatrolLogic();

            break;

            case States.Following:
            //perseguirle
            enemyFollowScr.FollowPlayer();

            break;

            case States.Leaving:
            

            break;
        }
    }

    public void SwitchState(int i)
    {
        switch (i)
        {
            default:
            //To Following
            case 0:
            currentState = States.Following;
            enemyFollowScr.enabled = true;

            break;

            //To Leaving
            case 1:

            
            break;

            //To Patrolling
            case 2:

            
            break;
        }
    }
}
