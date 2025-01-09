using UnityEngine;

public class FallingObstacleSpawner : MonoBehaviour
{
    [Header("????????")]
    public GameObject obstaclePrefab;

    [Header("????")]
<<<<<<< Updated upstream
    public float spawnInterval = 2f; // ???????

    [Header("????????")]
    public Vector3 spawnPosition = new Vector3(224.55f, 17f, -4.5f); // ??????

    [Header("????????")]
    public float destroyHeight = -10f; // ??????????
=======
    public float spawnInterval = 2f;

    [Header("????????")]
    public Vector3 spawnPosition = new Vector3(224.55f, 17f, -4.5f);

    [Header("??????")]
    public float destroyHeight = -10f;
>>>>>>> Stashed changes

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 0f, spawnInterval);
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("ObstaclePrefab ???????????");
            return;
        }

<<<<<<< Updated upstream
        // ??????????????????
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        Debug.Log($"???????????: {obstacle.transform.position}");
=======
        // ??????
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        // ??????????????????????????
        obstacle.transform.position = spawnPosition;
>>>>>>> Stashed changes

        // ????????????????
        StartCoroutine(DestroyObstacleAfterFall(obstacle));
    }

    private System.Collections.IEnumerator DestroyObstacleAfterFall(GameObject obstacle)
    {
        while (obstacle != null && obstacle.transform.position.y > destroyHeight)
        {
            yield return null;
        }

        if (obstacle != null)
        {
<<<<<<< Updated upstream
            Destroy(obstacle); // ??????
=======
            Destroy(obstacle);
>>>>>>> Stashed changes
            Debug.Log("???????????");
        }
    }
}
