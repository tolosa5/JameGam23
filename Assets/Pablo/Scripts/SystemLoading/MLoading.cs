using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MLoading : MonoBehaviour
{

    [SerializeField] private Slider sliderLoading;
    private void Start()
    {
        string LoadLvL = MSceneLoad.nextLVL;
        StartCoroutine(StartLoad(LoadLvL));
    }

    IEnumerator StartLoad(string NextScene)
    {
        yield return new WaitForSeconds(1f); //De momento esto para que dure la pantalla de carga

        AsyncOperation operation = SceneManager.LoadSceneAsync(NextScene);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progreso = Mathf.Clamp01(operation.progress / 3f);  //Falseamos la carga de escena, haciendo que carga la escena pero solo una parte del slider
            sliderLoading.value = progreso;
            MSceneLoad.valueLoadingSlider = progreso;
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
