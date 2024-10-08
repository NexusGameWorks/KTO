using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        // ????????????????????
        if (other.gameObject.CompareTag("DeadZone"))
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        transform.position = startPosition;
    }
}
