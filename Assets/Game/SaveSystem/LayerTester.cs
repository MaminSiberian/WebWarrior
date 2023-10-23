using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LayerTester : MonoBehaviour
{
    [SerializeField] private LayerMask wallsLayer;

    [Button]
    private void DebugLayerNumber()
    {
        Debug.Log(LayerMask.LayerToName(wallsLayer));
        Debug.Log(wallsLayer.value);
    }
}
