using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class goalDecisionScript : MonoBehaviour
{
    public bool isColliding = false;
    private void OnTriggerEnter(Collider other)
    {
        //  tag��goal��������
        if (other.CompareTag("Player"))
        {
            Debug.Log("goal");
            isColliding = true;
            Destroy(other.gameObject);
        }
    }

}
