using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : MonoBehaviour
{
    EnemyDetection enemyDetectionScr;
    EnemyFollow enemyFollowScr;
    //PatrolAI patrolScr;
    EnemyShoot enemyShoot;
    EnemyLeave enemyLeave;

    [HideInInspector] public enum States{Patrolling, Following};
    [HideInInspector] public States currentState;

    bool shooting;

    private void Start() 
    {
        enemyDetectionScr = GetComponent<EnemyDetection>();
        enemyFollowScr = GetComponent<EnemyFollow>();
        enemyLeave = GetComponent<EnemyLeave>();
        enemyShoot = GetComponentInChildren<EnemyShoot>();
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

            break;

            case States.Following:
            //perseguirle
            Debug.Log("siguiendo");
            if (!shooting)
            {
                shooting = true;
                StartCoroutine(enemyShoot.Shoot());
            }
            enemyFollowScr.LookAtPlayer();
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
            Debug.Log("a seguirle");
            currentState = States.Following;

            break;

            //To Patrolling
            case 1:
            currentState = States.Patrolling;
            
            break;
        }
    }
}
