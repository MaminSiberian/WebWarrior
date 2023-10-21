using System;
using UnityEngine;

public class TestPlayer : MonoBehaviour, IDamagable
{
    public static event Action OnPlayerDeath;

    public void GetDamage()
    {
        Debug.Log(name + " damaged");
    }
}
