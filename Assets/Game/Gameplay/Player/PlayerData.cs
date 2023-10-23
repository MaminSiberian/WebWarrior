using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    [Header("The force with which the enemy will be thrown")]
    [Space]
    [SerializeField] internal float forceToThrowObject;
    [Header("Maximum range of the hook")]
    [Space]
    [Range(2, 10)] [SerializeField] public float maxDistanseHook;
    [Space]
    [Header("Time intervals (it may be worth replacing with animation curve)")]
    [Header("throw, pullUp, time coyote, stun")]
    [Space]
    [Range(0f, 3f)] [SerializeField] public float timeThrowHook;
    [Range(0f, 3f)] [SerializeField] public float timePullUpHook;
    [Range(0, 1)] [SerializeField] public float timeCaiot;
    [Range(0, 5)] [SerializeField] public float timeStun;
    [Header("Parameters of the assistant in the guidance")]
    [Space]
    [Range(0, 30)] [SerializeField] public float triggerAngleAIM;
    [Range(0, 10)] [SerializeField] public int countRaiAIM;
    [SerializeField] public LayerMask layerToTouchAIM;
    [SerializeField] public LayerMask layerWall;
    [SerializeField] public LayerMask layerEnemyAndProjectile;

    private void OnValidate()
    {
        Debug.Log(1);
        EventSystem.SendDataPlayerChanged();
    }
}
