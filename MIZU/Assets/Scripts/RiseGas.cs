using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �K�v�ȃR���|�[�l���g�������I�ɃA�^�b�`
[RequireComponent(typeof(Collider))]

public class RiseGas : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 5f;   //  �㏸���x
    [SerializeField] private float riseHeight = 30f; //  �㏸���鍂��
    [SerializeField] private bool isRising = false;

    private Vector3 initialPosition;

    [SerializeField] private MM_PlayerPhaseState _pState;

    void Start()
    {
        // �K�v�ȃR���|�[�l���g�̎擾
        _pState = GetComponent<MM_PlayerPhaseState>();

        // �G���[�`�F�b�N
        if (_pState == null)
        {
            Debug.LogError("MM_PlayerPhaseState �R���|�[�l���g��������܂���");
        }

        // �����ʒu��ۑ�
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //  �g���K�[�ɓ������I�u�W�F�N�g��riseGas�̃^�O�������Ă��āARotateObject����]���̏ꍇ
        if (other.CompareTag("riseGas"))
        {
            isRising = true;
            Debug.Log("RiseGas started rising due to trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //  �g���K�[����o���I�u�W�F�N�g��riseGas�̃^�O�������Ă���A�܂��� RotateObject����]���~�����ꍇ
        if (other.CompareTag("riseGas"))
        {
            isRising = false;
            Debug.Log("RiseGas stopped rising due to trigger exit or rotation stopped.");
        }
    }

    void Update()
    {
       

        if (isRising && _pState.GetState() == MM_PlayerPhaseState.State.Gas)
        {
            //  �㏸
            float step = riseSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position + Vector3.up * step;
            transform.position = newPosition;

            //  �ڕW�����ɒB������㏸���~
            Vector3 targetPosition = initialPosition + Vector3.up * riseHeight;
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isRising = false;
                Debug.Log($"�v���C���[�͖ڕW�̍����܂ŏ㏸�����̂ŁA�~�܂�܂��B");
            }
        }
    }
}
