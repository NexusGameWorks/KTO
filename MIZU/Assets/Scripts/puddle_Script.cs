using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MM_PlayerPhaseState))]

public class puddle_Script : MonoBehaviour
{

    private float contactTime = 0f;  //  �ڐG��������
    private bool isColliding = false;  //  �I�u�W�F�N�g���ڐG���Ă��邩�̃t���O
    public float destroyTime = 2f;  //  �I�u�W�F�N�g���j�󂳂�鎞��

    MM_PlayerPhaseState _pState;

    private void OnCollisionEnter(Collision collision)
    {
        if(_pState.GetState() == MM_PlayerPhaseState.State.Liquid)
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("puddle"))
        {
            isColliding = false;
            contactTime = 0f;  //  ���ꂽ��ڐG���Ԃ����Z�b�g����
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isColliding)
        {
            contactTime += Time.deltaTime;

            if(contactTime >= destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
