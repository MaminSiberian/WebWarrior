using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlayerController : MonoBehaviour, IDamagable
{
    public void GetDamage()
    {
        Debug.Log("Player has damaged");
        EventSystem.SendPlayerDeath();
    }
}
