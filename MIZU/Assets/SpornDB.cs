using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab; // �_���{�[����Prefab
    public Transform spawnPoint; // �X�|�[���ʒu
    public float spawnInterval = 2f; // �X�|�[���Ԋu

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // ���Ԋu�Ń_���{�[���𐶐�
        if (timer >= spawnInterval)
        {
            SpawnBox();
            timer = 0f;
        }
    }

    void SpawnBox()
    {
        if (boxPrefab != null && spawnPoint != null)
        {
            // �X�|�[���ʒu��Prefab�𐶐�
            Instantiate(boxPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}