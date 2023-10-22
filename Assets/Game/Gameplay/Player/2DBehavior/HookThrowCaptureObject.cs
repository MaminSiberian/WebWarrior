using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookThrowCaptureObject : IHookBehavior
    {
        private HookController hc;

        public HookThrowCaptureObject(HookController hc)
        {
            this.hc = hc;
        }


        public void Enter()
        {
            Debug.Log("Enter HookThrowCaptureObject state");
            hc.capturedTarget.GetComponent<Rigidbody2D>().AddForce(hc.direction * hc.forceToThrowObject, ForceMode2D.Impulse);
            hc.SetBehaviorRotation();
        }

        public void Exit()
        {
            hc.capturedTarget = null;
            hc.isEndHook = true;
        }

        public void UpdateBehavior()
        {
        }
    }

}