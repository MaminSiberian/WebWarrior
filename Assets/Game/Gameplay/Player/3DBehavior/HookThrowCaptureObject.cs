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
            //Debug.Log("Enter HookThrowCaptureObject state");
            EventSystem.SendHookThrowObject();
            hc.capturedTarget.GetComponent<Rigidbody>().AddForce(hc.direction * hc.forceToThrowObject, ForceMode.Impulse);
            hc.SetBehaviorRotation();
            hc.rb.velocity = Vector3.zero;
        }

        public void Exit()
        {
            hc.grabableTarget.OnRelease();
            hc.grabableTarget = null;
            hc.capturedTarget = null;
            hc.isEndHook = true;
        }

        public void UpdateBehavior()
        {
        }
    }

}