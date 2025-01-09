using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    public float speed = 2f;      // �ړ����x
    public float distance = 5f;  // �ړ�����
    private Vector3 startPosition;
    private bool isMoving = true;

    void Start()
    {
        // �����ʒu���L�^
        startPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            // ���ԂɊ�Â��ăX���[�Y�Ɉړ�
            float offset = Time.time * speed;
            transform.position = startPosition + new Vector3(offset, 0, 0);

            // �ړ��������w��l�𒴂�����I��
            if (Vector3.Distance(startPosition, transform.position) >= distance)
            {
                isMoving = false;
                Destroy(gameObject); // �I�u�W�F�N�g���폜
            }
        }
    }
}

//����Ń_���{�[�������Ǝv��

//�ړ�������ύX�������ꍇ�́Anew Vector3�̒l��new Vector3(0, offset, 0)�i�c�ړ��j��new Vector3(0, 0, offset)�i���s���ړ��j�ɕύX����
//�_���{�[���v���n�u�����Ă��̃X�N���v�g�A�^�b�`����