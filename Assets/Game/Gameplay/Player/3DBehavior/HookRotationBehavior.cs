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
            //Debug.Log("Enter Rotation state");
            // hc.currentMaxDistanceHook = hc.maxDistanseHook;
        }

        public void Exit()
        {
            //Debug.Log("Exit Rotation state");
        }

        public void UpdateBehavior()
        {
            Rotation();
        }

        private void Rotation()
        {
            hc.transform.rotation = Quaternion.LookRotation(hc.direction);

            if ((hc.isActiveHook) && (hc.isEndHook))
            {
                hc.isActiveHook = false;
                hc.isEndHook = false;
                hc.SetBehaviorAIM();
            }
        }
    }
}