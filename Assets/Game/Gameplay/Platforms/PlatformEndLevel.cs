using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEndLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Debug.Log("Level complitte");
    }
}
