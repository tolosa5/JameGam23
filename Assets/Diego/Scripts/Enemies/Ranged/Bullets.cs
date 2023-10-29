using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] float speed;
    Transform player;
    Vector3 aimedPoint;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //aimedPoint = new Vector3(transform.position.x, -transform.position.y) - new Vector3(player.position.x, -player.position.y);
        aimedPoint = player.transform.position - transform.position;

        Destroy(gameObject, 2);
    }

    void Update()
    {
        transform.Translate(aimedPoint.normalized * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().GetHit(1);
        }
    }
}
