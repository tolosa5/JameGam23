using UnityEngine;

public class Camera_Test : MonoBehaviour
{
    public Transform playerTarget;


    private void Update()
    {
        transform.position = new Vector3(playerTarget.position.x, playerTarget.position.y, -10);
    }
}
