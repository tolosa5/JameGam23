using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject parentRoom;
    public List<Transform> SpawnPoints = new List<Transform>();

    private int indexRoomParent;


    public IEnumerator SystemSpawnEnemy()
    {

        if (this.gameObject.transform.parent.name != "RoomManager")
        {
            FindIndexListRoom();
        }
        else
        {
            SpawnEnemysOrigenRoom();
        }

        yield return new WaitForSeconds(0.1f);
    }

    public void FindIndexListRoom()
    {

        for (int i = 0; i < P_GameMangaer.managerInstance.rooms.Count; i++)
        {
            if (parentRoom == P_GameMangaer.managerInstance.rooms[i])
            {
                indexRoomParent = P_GameMangaer.managerInstance.rooms.IndexOf(parentRoom);
            }
        }

        SpawnEnemysInRooms();
    }


    public void SpawnEnemysInRooms()
    {
        switch (indexRoomParent)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }


    public void SpawnEnemysOrigenRoom()
    {

    }

}
