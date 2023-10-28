using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootSpot;
    bool activated;

    public IEnumerator Shoot()
    {
        while (true)
        {
            Debug.Log("disparando");
            Instantiate(bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
