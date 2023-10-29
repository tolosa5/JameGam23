using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int lifes;
    Animator anim;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }

    public void GetHit()
    {
        lifes--;
        if (lifes <= 0)
        {
            EnemyDeath();
        }
    }

    public void GetStronger(int i)
    {
        switch (i)
        {
            default:
            case 1:

            break;
        }
    }

    void EnemyDeath()
    {
        Destroy(this.gameObject,1f);
        anim.SetTrigger("Die");
    }
}
