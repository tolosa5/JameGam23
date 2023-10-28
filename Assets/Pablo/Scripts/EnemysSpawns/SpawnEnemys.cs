using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject parentRoom;
    public List<Transform> SpawnPoints = new List<Transform>();

    private int indexRoomParent;
    private int randEnemysToSpawn;
    private int randSelectEnemy;

    private void Start()
    {
        StartCoroutine(SystemSpawnEnemy());   
    }

    public IEnumerator SystemSpawnEnemy()
    {
        yield return new WaitForSeconds(1);

        if (this.gameObject.transform.parent.name != "RoomOrigen")
        {
            FindIndexListRoom();
            Debug.Log("Spawn in other rooms");
        }
        else
        {
            SpawnEnemysOrigenRoom();
            Debug.Log("Spawn enemys in Origen");
        }

        P_GameMangaer.managerInstance.gameReady = true;
    }

    public void FindIndexListRoom()
    {

        for (int i = 0; i < P_GameMangaer.managerInstance.GeneratedRooms.Count; i++)
        {
           
            if (parentRoom.name == P_GameMangaer.managerInstance.GeneratedRooms[i].name)
            {
                indexRoomParent = P_GameMangaer.managerInstance.GeneratedRooms.IndexOf(parentRoom);
            }

        }

        SpawnEnemysInRooms();
    }


    public void SpawnEnemysInRooms()
    {
        switch (indexRoomParent)
        {
            case 0:

                
                randEnemysToSpawn = Random.Range((int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.x, (int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.y);


                for (int i = 0; i < randEnemysToSpawn; i++)
                {           
                    Instantiate(P_GameMangaer.managerInstance.enemysGO[Random.Range(0,2)], SpawnPoints[Random.Range(0,SpawnPoints.Count)].position, Quaternion.identity);
                }
                

                break;

            case 1:

                randEnemysToSpawn = Random.Range((int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.x, (int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.y);


                for (int i = 0; i < randEnemysToSpawn; i++)
                {
                    Instantiate(P_GameMangaer.managerInstance.enemysGO[Random.Range(0, 3)], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
                }


                break;

            case 2:

                randEnemysToSpawn = Random.Range((int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.x, (int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.y);


                for (int i = 0; i < randEnemysToSpawn; i++)
                {
                    Instantiate(P_GameMangaer.managerInstance.enemysGO[Random.Range(1, 4)], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
                }


                break;

            case 3:
                
                randEnemysToSpawn = Random.Range((int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.x, (int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.y);


                for (int i = 0; i < randEnemysToSpawn; i++)
                {
                    Instantiate(P_GameMangaer.managerInstance.enemysGO[Random.Range(2, 4)], SpawnPoints[Random.Range(0, SpawnPoints.Count)].position, Quaternion.identity);
                }
                
                break;
        }

    }


    public void SpawnEnemysOrigenRoom()
    {

        Instantiate(P_GameMangaer.managerInstance.enemysGO[0], SpawnPoints[0].transform.position, Quaternion.identity);

    }

}
