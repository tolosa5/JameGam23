using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Temp : MonoBehaviour
{
    public GameObject goWalls;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        TestEndGame();
    }

    public void TestEndGame()
    {
        P_GameMangaer.managerInstance.endGame = true;
        goWalls.SetActive(false);
    }

}
