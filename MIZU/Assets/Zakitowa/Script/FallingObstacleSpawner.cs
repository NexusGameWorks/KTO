using UnityEngine;

public class FallingObstacleSpawner : MonoBehaviour
{
    [Header("????????")]
    public GameObject obstaclePrefab; // ???????????????????

    [Header("????")]
    public Transform spawnPoint; // ??????????

    [Header("????")]
    public float spawnInterval = 2f; // ???????

    [Header("????????")]
    public float destroyHeight = -10f; // ??????????

    private void Start()
    {
        // ???????????
        InvokeRepeating(nameof(SpawnObstacle), 0f, spawnInterval);
    }

    private void SpawnObstacle()
    {
        // ??????
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);

        // ????????????????
        StartCoroutine(DestroyObstacleAfterFall(obstacle));
    }

    private System.Collections.IEnumerator DestroyObstacleAfterFall(GameObject obstacle)
    {
        // ?????????????????
        while (obstacle != null && obstacle.transform.position.y > destroyHeight)
        {
            yield return null; // ?????????
        }

        if (obstacle != null)
        {
            Destroy(obstacle); // ??????
        }
    }
}
