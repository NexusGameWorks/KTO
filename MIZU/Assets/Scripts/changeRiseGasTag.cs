using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class changeRiseGasTag : MonoBehaviour
{
    [Header("��]��Ԃɉ����ă^�O��t�^�E��������Ώۂ́uriseGas�v�I�u�W�F�N�g")]
    [SerializeField] private GameObject riseGusObject;

     private RotateObject rotateObject;


    void Awake()
    {
        rotateObject = GetComponent<RotateObject>();

        if (rotateObject == null)
        {
            Debug.LogError($"RotateObject��{gameObject.name}�Ɍ�����܂���B");
            return;
        }

        if (riseGusObject == null)
        {
            Debug.LogError($"RiseGasObject��{gameObject.name}�ɐݒ肳��Ă��܂���B");
            return;
        }

        //  RotateObject�̃C�x���g��ǂ�
        rotateObject.RotatingChange += RotateStateChange;

        //  ������ԂŃ^�O��ݒ肷��
        UpdateRiseGasTag(rotateObject.IsRotating);
    }

    //  �X�N���v�g���j������鎞�ɁAAwake()�̃C�x���g��ǂލs�ׂ�j������
    void OnDestroy()
    {
        if (rotateObject != null)
        {
            rotateObject.RotatingChange -= RotateStateChange;
        }
    }

    //  RotateObject�̉�]��Ԃ��ω������ۂɌĂяo����郁�\�b�h
    private void RotateStateChange(RotateObject rotate, bool isRotating)
    {
        UpdateRiseGasTag(isRotating);
    }

    private void UpdateRiseGasTag(bool isRotating)
    {
        if(isRotating) 
        {
            //  ���ł�"riseGas"�̃^�O���t���Ă���ꍇ�A�������Ȃ�
            if (riseGusObject.CompareTag("riseGas")) return;

            //  "riseGas"�̃^�O�̕t�^
            riseGusObject.tag = "riseGas";
            Debug.Log($"{riseGusObject.name}��'riseGas'�̃^�O��t�^�����B");
            return;
        }

        //  "riseGas"�̃^�O���t���Ă��Ȃ��ꍇ�A�������Ȃ�
        if (!riseGusObject.CompareTag("riseGas")) return;

        //  "Untagged"�̃^�O�ɕύX����
        riseGusObject.tag = "Untagged";
        Debug.Log($"{riseGusObject.name}����'riseGas'�̃^�O�������܂����B");
    }
}
