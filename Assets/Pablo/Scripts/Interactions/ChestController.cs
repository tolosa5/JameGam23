using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen;
    

    public void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("Chest is open");
        }
    }
}
