using UnityEngine;

public class WaterControl : MonoBehaviour
{
    public GameObject water; // �㉺���鐅�I�u�W�F�N�g
    public float ascendAmount = 1.0f; // �㏸���̈ړ���
    public float descendAmount = 1.0f; // ���~���̈ړ���
    public float moveSpeed = 2.0f; // �ړ����x
    public bool isAscending = true; // �㏸���邩���~���邩(True�Ȃ�オ��)

    private Vector3 targetPosition; // ���̖ڕW�ʒu
    void Start()
    {
        // �����ʒu��ݒ�
        targetPosition = water.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �v���C���[���G�ꂽ�ꍇ�̂ݓ���
        {
            // �㏸�܂��͉��~����ڕW�ʒu��ݒ�
            float direction = isAscending ? ascendAmount : -descendAmount;
            targetPosition = water.transform.position + new Vector3(0, direction, 0);
        }
        Collider col = this.GetComponent<Collider>();
        col.enabled = false;
    }

    void Update()
    {
        // ����ڕW�ʒu�Ɍ������Ĉړ�������
        water.transform.position = Vector3.Lerp(water.transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
