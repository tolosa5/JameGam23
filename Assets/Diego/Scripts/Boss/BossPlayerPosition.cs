using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerPosition : MonoBehaviour
{
    float timerStay;
    float timerLeft;

    bool activated;
    bool left;
    bool bossInside;
    Animator anim;
    GameObject boss;

    private void Start() 
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        anim = boss.GetComponent<Animator>();
    }

    private void Update() 
    {
        if (activated && !left && !bossInside)
        {
            timerStay += Time.deltaTime;
            if (timerStay >= 2)
            {
                Debug.Log("parriba");
                StartCoroutine(Wait());
            }
        }
        else if (left)
        {
            timerLeft += Time.deltaTime;
            if (timerLeft >= 2)
            {
                Debug.Log("se fue seguro");
                activated = false;
                timerStay = 0;
                timerLeft = 0;
                if (bossInside)
                {
                    Debug.Log("pabajo");
                    anim.SetTrigger("Down");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.transform.CompareTag("Player"))
        {
            left = false;
            activated = true;
        }
        else if(other.transform.CompareTag("Boss"))
        {
            bossInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.transform.CompareTag("Player"))
        {
            left = true;
        }
        else if(other.transform.CompareTag("Boss"))
        {
            bossInside = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Jump");
        timerStay = 0;
    }

    /*
    public void OnTriggerStay2D(Collider2D other) 
    {
        if (other.transform.CompareTag("Player"))
        {
            timerStay += Time.deltaTime;
            if (timerStay >= 2)
            {
                Debug.Log("parriba");
                StartCoroutine(Wait());
            }
        }
        else if (timerStay != 0)
        {
            timerLeft += Time.deltaTime;
            if (timerLeft >= 2)
            {
                timerStay = 0;
            } 
        }
    }
    */
}
