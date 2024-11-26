using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_With_Moving_Trigger : MonoBehaviour
{
    [SerializeField, Header("乗られる方,親(足場など)")]
    private Transform movevParent;
    [SerializeField,Header("乗る方,子(自分)")]
    private Transform withChild;


    string MOVE_GROUND = "MoveGround";

    private void OnTriggerEnter(Collider other)
    {
        print("TriggerName=" + other.name);

        if (other.gameObject.CompareTag(MOVE_GROUND))
        {
            print("接続");
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
            print("解除");
            withChild.parent=null;
            //withChild.transform.SetParent(null);
        }
    }
}
