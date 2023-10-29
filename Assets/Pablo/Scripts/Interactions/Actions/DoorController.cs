using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpen;
    public Interactable interactable;
    public GameObject textPressEToOpen;
    public GameObject textNeedAKey;

    public void OpenDoor()
    {
        if (!isOpen)
        {
            //KeyManager testInteract = go.GetComponent<KeyManager>();

            if (GameManager.instance.haveKey)
            {
                isOpen = true;
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                GameManager.instance.haveKey = false;
            }
            else
            {
                Debug.Log("Need a Key");
            }

        }
    }

    private void Update()
    {

        switch(GameManager.instance.haveKey)
        {

            case true:

                if (interactable.isInRange && !isOpen)
                {
                    textPressEToOpen.SetActive(true);
                }
                else
                {
                    textPressEToOpen.SetActive(false);
                }
                break;

            case false:

                if (interactable.isInRange && !isOpen)
                {
                    textNeedAKey.SetActive(true);
                }
                else
                {
                    textNeedAKey.SetActive(false);
                }

                break;
        }

    }
}
