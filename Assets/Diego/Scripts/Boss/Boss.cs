using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Transform player;
    public bool isFlipped = false;
    GameObject closeRoomGO;
    CloseRoom closeRoomScr;
    Animator anim;

    private void Start() 
    {
        closeRoomGO = GameObject.FindGameObjectWithTag("Player");
        closeRoomScr = GetComponent<CloseRoom>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void GoIntro()
    {
        anim.SetTrigger("Intro");
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
