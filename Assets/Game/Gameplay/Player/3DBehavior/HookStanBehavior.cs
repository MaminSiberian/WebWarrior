using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookStanBehavior : IHookBehavior
    {
        private HookController hc;
        private float currentTimeStan;

        public HookStanBehavior(HookController hc)
        {
            this.hc = hc;
        }
        public void Enter()
        {
            Debug.Log("Enter HookStabBehavior state");
            EventSystem.SendHookStan();
            currentTimeStan = hc.timeStan;
        }

        public void Exit()
        {
            Debug.Log("Exit HookStabBehavior state");
            hc.capturedTarget = null;
            hc.isEndHook = true;
        }

        public void UpdateBehavior()
        {
            currentTimeStan -= Time.deltaTime;
            if (currentTimeStan <=0 )
            {
                hc.SetBehavior(hc.behaviorPrevios);
            }
        }
    }
}