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
            //Debug.Log("Enter RotationWithObjectBehavior state");
            // hc.currentMaxDistanceHook = hc.maxDistanseHook;
        }

        public void Exit()
        {
            //Debug.Log("Exit RotationWithObjectBehavior state");
        }

        public void UpdateBehavior()
        {
            Rotation();
        }

        private void Rotation()
        {
            hc.transform.rotation = Quaternion.LookRotation(hc.direction);          
            hc.capturedTarget.transform.position = hc.hook.transform.position;

            if ((hc.isActiveHook) && (hc.icCaptureSomthing))
            {
                hc.isActiveHook = false;
                hc.SetBehaviorThrowCaptureObject();
            }
        }
    }
}
