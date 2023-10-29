using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] int attackDamage = 20;

    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackMask;

    [SerializeField] Transform attackSpot;

    public void Attack()
    {
        Collider2D coll = Physics2D.OverlapCircle(attackSpot.position, attackRange, attackMask);
        if (coll != null)
        {
            coll.GetComponent<Player>().GetHit(2);
        }
    }
}
