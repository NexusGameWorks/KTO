using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_With_Moving_Trigger : MonoBehaviour
{
    [SerializeField, Header("������,�e(����Ȃ�)")]
    private Transform movevParent;
    [SerializeField,Header("����,�q(����)")]
    private Transform withChild;


    string MOVE_GROUND = "MoveGround";

    private void OnTriggerEnter(Collider other)
    {
        print("TriggerName=" + other.name);

        if (other.gameObject.CompareTag(MOVE_GROUND))
        {
            print("�ڑ�");
            movevParent = other.transform;
            withChild.parent = movevParent;
            //withChild.transform.SetParent(movevParent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("TriggerName=" + other.name);
        if (other.gameObject.CompareTag(MOVE_GROUND))
        {
            print("����");
            withChild.parent=null;
            //withChild.transform.SetParent(null);
        }
    }
}
