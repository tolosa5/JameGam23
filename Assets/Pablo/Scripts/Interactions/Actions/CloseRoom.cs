using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRoom : MonoBehaviour
{
    public GameObject goWalls;
    [SerializeField] GameObject doorGO;
    [SerializeField] GameObject wallGO;

    Animator anim;

    bool enemy;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si detecta al player y al enemigo (independientemente del n�mero de enemigos),
        //activa la puerta
        if (other.gameObject.CompareTag("Boss"))
        {
            enemy = true;
        }
        if (other.gameObject.CompareTag("Player") && enemy == true)
        {
            anim.SetTrigger("Close");
            Debug.Log("David Cage");
            //doorGO.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Si no detecta al enemigo, se desactiva la puerta.
        //IMPORTANTE HACER QUE EL ENEMIGO SALGA DEL TRIGGER ANTES DE DESTRUIRLO
        if (other.gameObject.CompareTag("Boss"))
        {
            if (doorGO.activeInHierarchy)
            {
                Debug.Log("salio");
                enemy = false;
                anim.SetTrigger("Open");
                //doorGO.SetActive(false);
            }
        }
    }
}
