using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_GameMangaer : MonoBehaviour
{
    public static P_GameMangaer managerInstance;

    [Space(10)]
    public bool worldGenerated;
    public bool gameReady;
    public bool endGame;

    public List<GameObject> rooms = new List<GameObject>();
    public List<GameObject> GeneratedRooms = new List<GameObject>();

    public List<GameObject> enemysGO = new List<GameObject>();

    //public List<Transform> EnemysSpawns = new List<Transform>();  //------------------> New System Find Spawn Enemy Points <-----------------------

    public Transform posStartGeneration;

    public int limiteRooms = 4;

    public float offsetModuloY;

    [Header("X: Min , Y: Max")]
    public Vector2 enemysMinMaxSpawn;

    //private int rand; //SOLO SI GENERAMOS UNA ROOM AL INICO


    private void Awake()
    {
        #region Singleton

        if (managerInstance == null)
            managerInstance = this;
        else
            Destroy(this.gameObject);

        //DontDestroyOnLoad(managerInstance);

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
