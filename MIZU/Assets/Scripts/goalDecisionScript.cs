using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalDecisionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //  tag��goal��������
        if (other.CompareTag("goal"))
        { 
        Debug.Log("goal");
        Destroy(gameObject);
        }
    }

}
