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
        aimedPoint = transform.position - player.position;
    }

    void Update()
    {
        transform.Translate(aimedPoint * (speed * Time.deltaTime));
    }
}
