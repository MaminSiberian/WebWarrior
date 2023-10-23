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
            currentTimeStan = hc.timeStun;
        }

        public void Exit()
        {
            Debug.Log("need test winth enemy");
            //Debug.Log("Exit HookStabBehavior state");
            //hc.capturedTarget = null;
            //hc.isEndHook = true;
        }

        public void UpdateBehavior()
        {
            currentTimeStan -= Time.deltaTime;
            if (currentTimeStan <= 0)
            {
                hc.SetBehavior(hc.behaviorPrevios);
            }
        }
    }
}