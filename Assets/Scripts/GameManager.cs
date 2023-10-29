using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Vector3 playerStartPosition;

    [Header("Tutorial")]
    public GameObject tutorialPanel;
    [SerializeField] GameObject txtsTutorialMov;
    [SerializeField] GameObject txtsTutorialHook;

    [SerializeField] GameObject deathPanel, winPanel;

    [Header("WinPanel")]
    [SerializeField] AudioClip[] sfx;
    AudioSource aS;

    int coins;

    [HideInInspector] public bool tutorialAcabado;

    float score;
    float finalScore;

    [HideInInspector] public int timesLoaded;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        aS = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //playerStartPosition = Player.instance.transform.position;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        timesLoaded++;
        score = 0;
        finalScore = 0;
        coins = 0;
        deathPanel.SetActive(false);
        winPanel.SetActive(false);

        //Player.instance.gameObject.transform.position = new Vector3(8.54f, 6.01f);

        if (scene.buildIndex == 0)
        {
            Player.instance.gameObject.SetActive(false);
            score = 0;
        }
        else
        {
            if (!Player.instance.gameObject.activeSelf)
            {
                Player.instance.gameObject.SetActive(true);
                Player.instance.lifes = 3;
            }
        }

        if (timesLoaded <= 1)
        {
            StartCoroutine(TutorialManager());
        }
    }

    public int GetTimesLoaded()
    {
        return timesLoaded;
    }

    public void FailedTutorial()
    {
        timesLoaded--;
        SceneManager.LoadScene(1);
    }

    private void Update()
    {


        score += (int)(Time.deltaTime * 150);
        //Debug.Log(score);
       // scoreTxt.text = "Score: " + score.ToString();
    }

    IEnumerator TutorialManager()
    {
        tutorialPanel.SetActive(true);
        yield return null;
        StartCoroutine(TutorialPlayer());
    }

    IEnumerator TutorialPlayer()
    {
        Debug.Log("Dentro player");
        StartCoroutine(Pauses(txtsTutorialMov));
        yield return new WaitForSeconds(0.5f); //
        yield return new WaitUntil(() => Time.timeScale == 1);

        StartCoroutine(TutorialHook());
    }

    IEnumerator TutorialHook()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(Pauses(txtsTutorialHook));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Time.timeScale == 1);

        tutorialPanel.SetActive(false);
        tutorialAcabado = true;
    }

    IEnumerator Pauses(GameObject aux)
    {

        Debug.Log("dentro pauses");
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(0.5f); //espera tiempo
        aux.SetActive(true);
        yield return new WaitForSecondsRealtime(3f); //tiempo de texto
        aux.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f); //despues de tiempo

        Time.timeScale = 1;
        Debug.Log("fuera pauses");
    }

    public void DeathPanelActivate()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeathPanelDeactivate()
    {
        deathPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GetCoin()
    {
        coins++;
        score += 1000;
        aS.PlayOneShot(sfx[0]);
    }

    public void MultipyCoin(int mult)
    {
        score *= mult;
    }

    public void SetScore()
    {

    }

    public IEnumerator Win()
    {
        Debug.Log("Win activada");
        aS.PlayOneShot(sfx[1]);
        yield return new WaitForSeconds(1f);
        //toda l apesca de ganar y tal
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
