using System.Collections;
using UnityEngine;

public class Rooms : MonoBehaviour
{

    public int openSide;
    private int rand;
    private bool spawned = false;


    private void Awake()
    {

    }

    void Start()
    {

        Invoke("Spawn", 0.01f);

    }


    public void Spawn()
    {

        if (!spawned && P_GameMangaer.managerInstance.GeneratedRooms.Count <= P_GameMangaer.managerInstance.limiteRooms)
        {

            if (openSide == 1)
            {

                Debug.Log("Generamos Habitacion, Total de habitaciones generadas: " + (P_GameMangaer.managerInstance.GeneratedRooms.Count + 1));
                rand = Random.Range(0, P_GameMangaer.managerInstance.rooms.Count);
                Instantiate(P_GameMangaer.managerInstance.rooms[rand], transform.position + new Vector3(0,P_GameMangaer.managerInstance.offsetModuloY,0), P_GameMangaer.managerInstance.rooms[rand].transform.rotation);
                P_GameMangaer.managerInstance.rooms.RemoveAt(rand);

            }

            if (P_GameMangaer.managerInstance.GeneratedRooms.Count == P_GameMangaer.managerInstance.limiteRooms)
            {

                P_GameMangaer.managerInstance.worldGenerated = true;

                Debug.Log("End of Generate World");

            }

            spawned = true;

        }

    }

}