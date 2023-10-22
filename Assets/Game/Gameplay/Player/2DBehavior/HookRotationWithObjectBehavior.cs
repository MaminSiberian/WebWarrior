using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HookControl
{
    public class HookRotationWithObjectBehavior : IHookBehavior
    {
        private HookController hc;

        public HookRotationWithObjectBehavior(HookController hc)
        {
            this.hc = hc;
        }


        public void Enter()
        {
            Debug.Log("Enter RotationWithObjectBehavior state");
            // hc.currentMaxDistanceHook = hc.maxDistanseHook;
        }

        public void Exit()
        {
            Debug.Log("Exit RotationWithObjectBehavior state");
        }

        public void UpdateBehavior()
        {
            Rotation();
        }

        private void Rotation()
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = hc.mainCamera.ScreenToWorldPoint(mousePos);

            hc.direction = new Vector2(mousePos.x - hc.transform.position.x,
               mousePos.y - hc.transform.position.y).normalized;

            hc.pivotHook.up = hc.direction;
            hc.capturedTarget.transform.position = hc.hook.transform.position;

            if ((hc.isActiveHook) && (hc.icCaptureSomthing))
            {
                hc.isActiveHook = false;
                hc.SetBehaviorThrowCaptureObject();
            }
        }
    }
}
