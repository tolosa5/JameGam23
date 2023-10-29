using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ChangeTOGameScene : MonoBehaviour
{
    [SerializeField] GameObject panelPrincipal;
    [SerializeField] GameObject panelOptions;
    [SerializeField] AudioMixer master;

    public void ChangeToGame()
    {
        MSceneLoad.LoadScene("TestMapa");
    }

    public void ControlAnim()
    {

    }

    public void OptionsPanel()
    {
        panelPrincipal.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void ButtonBack()
    {
        panelPrincipal.SetActive(true);
        panelOptions.SetActive(false);
    }

    public void MasterVolume(float volume)
    {
        master.SetFloat("masterVolume", volume);
        Debug.Log("master");
    }

    public void AppQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
