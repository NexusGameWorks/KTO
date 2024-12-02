using UnityEngine;

public class WaterControl : MonoBehaviour
{
    public GameObject water; // �㉺���鐅�I�u�W�F�N�g
    public float ascendAmount = 1.0f; // �㏸���̈ړ���
    public float descendAmount = 1.0f; // ���~���̈ړ���
    public float moveSpeed = 2.0f; // �ړ����x
    public bool isAscending = true; // �㏸���邩���~���邩(True�Ȃ�オ��)

    private bool isDescending = false;

    private bool isMoving = false; // ���݈ړ������ǂ���
    private Vector3 targetPosition; // ���̖ڕW�ʒu
    private Vector3 startPosition; // �ړ��J�n�ʒu
    private float moveProgress = 0f; // �ړ��i���i0�`1�j

    void Update()
    {
        if (isMoving)
        {
            // �ړ��i�����X�V
            moveProgress += Time.deltaTime * moveSpeed;

            // �i���Ɋ�Â��Ĉʒu����
            water.transform.position = Vector3.Lerp(startPosition, targetPosition, moveProgress);

            // �ړ��������������~
            if (moveProgress >= 1f)
            {
                isMoving = false;
                moveProgress = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving) // �ړ����łȂ��ꍇ�̂ݓ���
        {
            isDescending = false;
            // �ړ��J�n�ʒu�ƖڕW�ʒu��ݒ�
            startPosition = water.transform.position;
            float direction = isAscending ? ascendAmount : -descendAmount;
            targetPosition = startPosition + new Vector3(0, direction, 0);

            Collider col = this.GetComponent<Collider>();
            col.enabled = false;
            isDescending = true;
            // �ړ����J�n
            isMoving = true;
        }
    }
}
