using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HookControl;

public class TestHookControl : MonoBehaviour
{
    [SerializeField] private HookController hc;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hc.isActiveHook = true;
        }
    }
}
