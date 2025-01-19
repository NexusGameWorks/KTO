using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public Transform player;    
    public Vector3 offset;      

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
