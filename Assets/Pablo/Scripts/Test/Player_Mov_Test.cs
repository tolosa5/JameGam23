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

        transform.Translate(new Vector3(h * speedMov * Time.deltaTime,0,0));
    }
}
