using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject parentRoom;
    public List<Transform> SpawnPoints = new List<Transform>();

    private int indexRoomParent;


    private void Start()
    {
        StartCoroutine(SystemSpawnEnemy());   
    }

    public IEnumerator SystemSpawnEnemy()
    {
        yield return new WaitForSeconds(1f);

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

    }

    public void FindIndexListRoom()
    {

        for (int i = 0; i < P_GameMangaer.managerInstance.GeneratedRooms.Count; i++)
        {
           
            if (parentRoom.name == P_GameMangaer.managerInstance.GeneratedRooms[i].name)
            {
                Debug.Log("Index: " + P_GameMangaer.managerInstance.GeneratedRooms.IndexOf(parentRoom));
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

                Debug.Log("Room Name: " + parentRoom.name + ", Index de la lista: " + indexRoomParent);

                /*
                int randEnemysToSpawn = Random.Range((int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.x, (int)P_GameMangaer.managerInstance.enemysMinMaxSpawn.y);
                int randSelectEnemy = Random.Range(0, 1);
                for (int i = 0; i < randEnemysToSpawn; i++)
                {
                    Instantiate(P_GameMangaer.managerInstance.enemysGO[randSelectEnemy], SpawnPoints[Random.Range(0,SpawnPoints.Count)].position, Quaternion.identity);
                }
                */

                break;

            case 1:

                Debug.Log("Room Name: " + parentRoom.name + ", Index de la lista: " + indexRoomParent);

                break;

            case 2:

                Debug.Log("Room Name: " + parentRoom.name + ", Index de la lista: " + indexRoomParent);

                break;

            case 3:

                Debug.Log("Room Name: " + parentRoom.name + ", Index de la lista: " + indexRoomParent);

                break;
        }

        


    }


    public void SpawnEnemysOrigenRoom()
    {

        Instantiate(P_GameMangaer.managerInstance.enemysGO[0], SpawnPoints[0].transform.position, Quaternion.identity);

    }

}
