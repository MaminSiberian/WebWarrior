using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookCatchPointBehavior : IHookBehavior
    {
        private HookController hc;


        public HookCatchPointBehavior(HookController hc)
        {
            this.hc = hc;
        }


        public void Enter()
        {
            //Debug.Log("Enter CathPoint state");
            hc.trigerToPullUp = false;

            hc.normalazedPercentOfMaxDistance = AccessoryMetods.NormalizedPercentOfDistanceXZ(
                hc.capturedTarget.transform.position,
                hc.maxDistanseHook,
                hc.transform.position);

            hc.startPos = hc.defaultPointHook.position;
            //endPos = hc.direction.normalized * hc.maxDistanseHook * normalazedPercentOfMaxDistance + (Vector2)hc.transform.position;
            hc.endPos = hc.capturedTarget.transform.position;
            hc.current = 0;
        }

        public void Exit()
        {
            //Debug.Log("Exit CatchPoint state");
            hc.hook.position = hc.defaultPointHook.position;
            hc.capturedTarget = null;
            hc.isEndHook = true;
        }

        public void UpdateBehavior()
        {
            CathPoint();
        }

        private void CathPoint()
        {
            if (!hc.trigerToPullUp)
            {
                Forward();
            }
            else
            {
                Back();
            }
        }

        private void Back()
        {
            EventSystem.SendPullBackHook();
            hc.transform.position = Vector3.Lerp(hc.startPos, hc.endPos, hc.current);
            hc.current += Time.deltaTime / (hc.timePullUpHook * hc.normalazedPercentOfMaxDistance);
            if (hc.current < 1)
            {
                hc.hook.position = hc.endPos;
            }
            if (Vector3.Distance(hc.capturedTarget.transform.position, hc.transform.position) < 0.1f)
            {
                hc.transform.position = hc.endPos;
                //hc.hook.transform.position = hc.direction.normalized * hc.idleDistanseHook + hc.transform.position;

                hc.SetBehaviorRotation();
            }
        }

        private void Forward()
        {
            EventSystem.SendThrowHook();
            hc.hook.position = Vector3.Lerp(hc.startPos, hc.endPos, hc.current);
            hc.current += Time.deltaTime / (hc.timeThrowHook * hc.normalazedPercentOfMaxDistance);
            if (hc.current >= 1)
            {
                hc.hook.position = hc.endPos;
                hc.trigerToPullUp = true;
                hc.startPos = hc.transform.position;
                hc.endPos = hc.capturedTarget.transform.position;
                hc.current = 0;
            }
        }
    }
}
