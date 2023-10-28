using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mov_Test : MonoBehaviour
{
    public float speedMov;

    private float h, v;


    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(h,v,0).normalized * speedMov * Time.deltaTime);
    }
}
