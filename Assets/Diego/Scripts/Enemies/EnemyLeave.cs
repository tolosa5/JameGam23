using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeave : MonoBehaviour
{
    [SerializeField] float maxPatience = 2.5f;
    [SerializeField] float maxDistance = 10f;

    float timer;

    EnemyStateMachine stateMachine;
    Transform player;

    private void Start() 
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = maxPatience;
    }

    public void PatienceCalculator()
    {
        if (Vector3.Distance(transform.position, player.position) > maxDistance)
        {
            timer -= Time.deltaTime;
            Debug.Log("contando");
        }
        else
        {
            timer = maxPatience;
            Debug.Log("reseteando");
        }

        if (timer <= 0)
        {
            timer = maxPatience;
            Debug.Log("cambio");
            stateMachine.SwitchState(1);
        }
    }
}
