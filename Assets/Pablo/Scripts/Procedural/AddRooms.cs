using UnityEngine;

public class AddRooms : MonoBehaviour
{

    void Start()
    {

        P_GameMangaer.managerInstance.GeneratedRooms.Add(this.gameObject);
        transform.parent = P_GameMangaer.managerInstance.posStartGeneration.transform;

    }
}
