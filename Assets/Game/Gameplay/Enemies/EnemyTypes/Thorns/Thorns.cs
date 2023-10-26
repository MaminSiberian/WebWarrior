using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamagable>() != null)
        {
            Debug.Log(other + "ran into thorns");
            other.GetComponent<IDamagable>().GetDamage();
        }
    } 
}
