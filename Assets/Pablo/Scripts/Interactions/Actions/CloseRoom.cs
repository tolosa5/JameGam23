using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRoom : MonoBehaviour
{
    public GameObject goWalls;

    public void StartCloseSystem()
    {
        StartCoroutine(ClosingRoomWhileBoosUP());
    }

    public IEnumerator ClosingRoomWhileBoosUP()
    {
        yield return new WaitForSeconds(1f);
        goWalls.SetActive(true);
    }
}
