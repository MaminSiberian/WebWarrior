using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HookControl;

public class InputMouse : MonoBehaviour
{
    [SerializeField] private HookController hc;
    [SerializeField] internal Camera mainCamera;
    private Ray ray;

    private void Start()
    {
        hc = FindAnyObjectByType<HookController>();
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hc.isActiveHook = true;
        }

        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, hc.layerGround))
        {
            hc.direction = new Vector3(
                    raycastHit.point.x - hc.transform.position.x,
                    hc.transform.position.y,
                    raycastHit.point.z - hc.transform.position.z);
        }
    }
}
