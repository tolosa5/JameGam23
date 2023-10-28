using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootSpot;
    bool activated;

    private void Update() 
    {
        if (!activated)
        {
            StartCoroutine(Shoot());
        }
    }

    public IEnumerator Shoot()
    {
        activated = true;
        while (true)
        {
            Instantiate(bullet, shootSpot);
            yield return new WaitForSeconds(1f);
        }
    }
}
