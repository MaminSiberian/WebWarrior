using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookRotationBehavior : IHookBehavior
    {
        private HookController hc;

        public HookRotationBehavior(HookController hc)
        {
            this.hc = hc;
        }


        public void Enter()
        {
            Debug.Log("Enter Rotation state");
           // hc.currentMaxDistanceHook = hc.maxDistanseHook;
        }

        public void Exit()
        {
            Debug.Log("Exit Rotation state");
        }

        public void UpdateBehavior()
        {
            Rotation();
        }

        private void Rotation()
        {
            //3d
            Ray ray = hc.mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, hc.layerGround))
            {
                hc.test.position = raycastHit.point;
                var lookDir = new Vector3(
                    raycastHit.point.x,
                    hc.transform.position.y, // нужно ля того чтобы попорачивался только по оси Y
                    raycastHit.point.z);
                hc.transform.LookAt (lookDir);

                hc.direction = new Vector3(
                    raycastHit.point.x - hc.transform.position.x,
                    hc.transform.position.y,
                    raycastHit.point.z - hc.transform.position.z);
            }


            ////2D
            //Vector2 mousePos = Input.mousePosition;
            //mousePos = hc.mainCamera.ScreenToWorldPoint(mousePos);

            //hc.direction = new Vector2(mousePos.x - hc.transform.position.x,
            //   mousePos.y - hc.transform.position.y).normalized;

            //hc.pivotHook.up = hc.direction;

            if ((hc.isActiveHook) && (hc.isEndHook))
            {
                hc.isActiveHook = false;
                hc.isEndHook = false;
                hc.SetBehaviorAIM();
            }
        }
    }
}