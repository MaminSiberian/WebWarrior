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
            Vector2 mousePos = Input.mousePosition;
            mousePos = hc.mainCamera.ScreenToWorldPoint(mousePos);

            hc.direction = new Vector2(mousePos.x - hc.transform.position.x,
               mousePos.y - hc.transform.position.y).normalized;

            hc.pivotHook.up = hc.direction;

            if ((hc.isActiveHook) && (hc.isEndHook))
            {
                hc.isActiveHook = false;
                hc.isEndHook = false;
                hc.SetBehaviorAIM();
            }
        }
    }
}