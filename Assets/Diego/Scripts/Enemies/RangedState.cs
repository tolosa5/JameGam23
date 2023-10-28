using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : MonoBehaviour
{
    EnemyDetection enemyDetectionScr;
    //EnemyFollow enemyFollowScr;
    //PatrolAI patrolScr;
    EnemyShoot enemyShoot;
    EnemyLeave enemyLeave;

    [HideInInspector] public enum States{Patrolling, Following};
    [HideInInspector] public States currentState;

    private void Start() 
    {
        enemyDetectionScr = GetComponent<EnemyDetection>();
        //enemyFollowScr = GetComponent<EnemyFollow>();
        //patrolScr = GetComponent<PatrolAI>();
        enemyLeave = GetComponent<EnemyLeave>();
        enemyShoot = GetComponent<EnemyShoot>();
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

            break;

            case States.Following:
            //perseguirle
            Debug.Log("siguiendo");
            //enemyFollowScr.FollowPlayer();
            StartCoroutine(enemyShoot.Shoot());
            enemyLeave.PatienceCalculator();

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

            break;

            //To Patrolling
            case 1:
            currentState = States.Patrolling;
            
            break;
        }
    }
}
