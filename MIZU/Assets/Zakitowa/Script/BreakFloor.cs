using System.Collections;
using UnityEngine;

public class BreakFloor : MonoBehaviour
{
    public int requiredPlayers = 2;
    public float timeWindow = 1f;
    private int currentPlayerCount = 0;
    private float lastPlayerEnterTime = 0f;
    private bool isBroken = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentPlayerCount++;
            if (currentPlayerCount == 1)
            {
                lastPlayerEnterTime = Time.time;
            }
            CheckIfShouldBreak();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentPlayerCount--;
        }
    }

    private void CheckIfShouldBreak()
    {
        if (currentPlayerCount >= requiredPlayers && !isBroken)
        {
            if (Time.time - lastPlayerEnterTime <= timeWindow)
            {
                DestroyFloor();
            }
        }
    }

    private void DestroyFloor()
    {
        isBroken = true;
        gameObject.SetActive(false);
        Debug.Log("Floor has broken!");
    }
}
