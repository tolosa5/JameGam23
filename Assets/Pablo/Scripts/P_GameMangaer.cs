using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_GameMangaer : MonoBehaviour
{
    public static P_GameMangaer managerInstance;

    [Space(10)]
    public bool worldGenerated;

    public List<GameObject> rooms = new List<GameObject>();
    public List<GameObject> GeneratedRooms = new List<GameObject>();

    public Transform posStartGeneration;

    public int limiteRooms = 4;

    public float offsetModuloY;

    private int rand;

    private void Awake()
    {
        #region Singleton

        if (managerInstance == null)
            managerInstance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(managerInstance);

        #endregion
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene sceneLoad, LoadSceneMode modoCarga)
    {
        switch (sceneLoad.name)
        {
            default:
            case "Menu":

                break;

            case "TestMapa":

                break;
        }

    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    //   -----------------------------------------------------------------------


    /* SI QUEREMOS GENERAR UNA HABITACION EN EL AWAKE QUE SEA ALEATORIA
     * 
    public void OnStartLevel()
    {
        Debug.Log("Nivel 1");
        rand = Random.Range(0, rooms.Count);
        Instantiate(rooms[rand], transform.position, rooms[rand].transform.rotation);
        rooms.RemoveAt(rand);

    }
    */
}
