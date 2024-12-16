using System.Collections;
using UnityEngine;

public class FallingObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;  // ��Q���̃v���n�u
    public float fallSpeed = 5f;  // ��Q���̗������x
    public float respawnTime = 3f;  // ��Q���̕�������
    public Vector3 spawnPosition;  // ��Q�������������ʒu

    private GameObject currentObstacle;  // ���݃A�N�e�B�u�ȏ�Q��

    private void Start()
    {
        // �ŏ��̏�Q���𐶐�
        SpawnNewObstacle();
    }

    private void Update()
    {
        if (currentObstacle != null)
        {
            // ��Q���������鏈��
            currentObstacle.transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

            // ��Q������ʊO�ɗ������ꍇ�ɐV���ɐ���
            if (currentObstacle.transform.position.y < -10f)
            {
                Destroy(currentObstacle);  // ���݂̏�Q�����폜
                SpawnNewObstacle();  // �V������Q���𐶐�
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[����Q���ɐG���Ǝ��S
        if (collision.gameObject.CompareTag("Player"))
        {
            // �v���C���[�����S���鏈���i��: �w���X�����⃊�X�^�[�g�j
            collision.gameObject.GetComponent<PlayerController>().Die();

            // ��Q�����폜���ĐV������Q���𐶐�
            Destroy(currentObstacle);
            SpawnNewObstacle();
        }
    }

    private void SpawnNewObstacle()
    {
        // �V������Q���𐶐�
        currentObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
