using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MSceneLoad
{
    public static string nextLVL;
    public static float valueLoadingSlider;

    public static void LoadScene(string nameLvL)
    {
        Cursor.lockState = CursorLockMode.None;
        nextLVL = nameLvL;
        SceneManager.LoadScene("TestLoading");
    }
}
