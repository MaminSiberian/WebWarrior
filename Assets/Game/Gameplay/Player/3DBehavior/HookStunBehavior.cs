using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookStunBehavior : IHookBehavior
    {
        private HookController hc;
        private float currentTimeStan;

        public HookStunBehavior(HookController hc)
        {
            this.hc = hc;
        }
        public void Enter()
        {
            //Debug.Log("Enter HookStabBehavior state");
            EventSystem.SendHookStan();
            EventSystem.SendHookThrowObject();
            if ((hc.capturedTarget != null) && (hc.grabableTarget != null))
            {
                Debug.Log("Øn the state of the STUN, the force of the throw is reduced by half. Need tests");
                hc.capturedTarget.GetComponent<Rigidbody>().AddForce(hc.direction * hc.forceToThrowObject / 2, ForceMode.Impulse);
            }
            currentTimeStan = hc.timeStun;
        }

        public void Exit()
        {
            Debug.Log("need test winth enemy");
            //Debug.Log("Exit HookStabBehavior state");
            hc.capturedTarget = null;
            hc.isEndHook = true;
            hc.rb.velocity = Vector3.zero;
        }

        public void UpdateBehavior()
        {
            currentTimeStan -= Time.fixedDeltaTime;
            if (currentTimeStan <= 0)
            {
                hc.SetBehaviorRotation();
            }
        }
    }
}