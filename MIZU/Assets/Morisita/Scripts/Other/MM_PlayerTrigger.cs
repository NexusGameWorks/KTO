using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_PlayerTrigger : MonoBehaviour
{
    string PLAYER = "Player";
    private bool isTrigger = false;
    private Collider hitCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(PLAYER))
        {
            hitCollider = other;
            isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER))
        {
            isTrigger = false;
        }

    }

    public bool GetTrigger() { return isTrigger; }
    public Collider GethitCollider() { return hitCollider; }

}
