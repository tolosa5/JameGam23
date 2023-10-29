using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int lifes;
    Animator anim;
    [SerializeField] AnimatorOverrideController firstOverride, secondOverride, thirdOverride;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }

    public void GetHit()
    {
        lifes--;

        if (lifes <= (lifes * 3 / 4))
        {
            anim.runtimeAnimatorController = firstOverride;
        }
        else if (lifes <= (lifes/2))
        {
            Enrage();
            anim.runtimeAnimatorController = secondOverride;
        }
        else if (lifes <= (lifes / 4))
        {
            anim.runtimeAnimatorController = thirdOverride;
        }
        else if (lifes <= 0)
        {
            BossDeath();
        }
    }

    void Enrage()
    {
        
    }

    void BossDeath()
    {
        anim.SetTrigger("Death");
    }
}
