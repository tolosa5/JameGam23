using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] Transform[] waypoints;
    bool isWaiting;

    int currentWaypoint = 0;

    public void PatrolLogic()
    {
        
        Debug.Log(currentWaypoint);
        if(transform.position.x != waypoints[currentWaypoint].position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(waypoints[currentWaypoint].position.x, transform.position.y), 
            speed * Time.deltaTime);
        }
        else if(!isWaiting)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        isWaiting = true;

        yield return new WaitForSeconds(waitTime);
        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
        currentWaypoint = 0;

        Flip();
        isWaiting = false;
    }

    void Flip()
    {
        if (transform.position.x > waypoints[currentWaypoint].position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
