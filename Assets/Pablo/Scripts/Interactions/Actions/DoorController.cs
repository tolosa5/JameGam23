using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpen;
    

    public void OpenDoor(GameObject go)
    {
        if (!isOpen)
        {
            KeyManager testInteract = go.GetComponent<KeyManager>();

            if (testInteract)
            {
                if (testInteract.keyCount > 0)
                {
                    isOpen = true;
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    testInteract.UseKey();
                }
                else
                {
                    Debug.Log("Need a Key");
                }
            }

        }
    }
}
