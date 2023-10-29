using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen;
    public GameObject CanvasHelpOpen;
    public Interactable interactable;

    public void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            GameManager.instance.haveKey = true;
            Player.instance.GetHealed();
            Debug.Log("Chest is open");
        }
    }

    public void HUDHelp()
    {
        if (GetComponent<Interactable>().isInRange)
        {
            CanvasHelpOpen.SetActive(true);
        }
        else
        {
            CanvasHelpOpen.SetActive(false);
        }
    }

    private void Update()
    {
        if (interactable.isInRange && !isOpen)
        {
            CanvasHelpOpen.SetActive(true);
        }
        else
        {
            CanvasHelpOpen.SetActive(false);
        }
    }

}
