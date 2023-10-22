using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookCatchPointBehavior : IHookBehavior
    {
        private HookController hc;
        private Vector3 startPos;
        private Vector3 endPos;
        private float current;
        private float normalazedPercentOfMaxDistance; // коэффициент для дистанции и времени
        private bool trigerToPullUp = false; // разделяет бросок и притяжение

        public HookCatchPointBehavior(HookController hc)
        {
            this.hc = hc;
        }


        public void Enter()
        {
            Debug.Log("Enter CathPoint state");
            trigerToPullUp = false;
         
            normalazedPercentOfMaxDistance = AccessoryMetods.NormalizedPercentOfDistanceXZ(
                hc.capturedTarget.transform.position,
                hc.maxDistanseHook,
                hc.transform.position);

            startPos = hc.direction.normalized * hc.idleDistanseHook + hc.transform.position;
            //endPos = hc.direction.normalized * hc.maxDistanseHook * normalazedPercentOfMaxDistance + (Vector2)hc.transform.position;
            endPos = hc.capturedTarget.transform.position;
            current = 0;
        }

        public void Exit()
        {
            Debug.Log("Exit CatchPoint state");
            hc.capturedTarget = null;
            hc.isEndHook = true;
        }

        public void UpdateBehavior()
        {
            CathPoint();
        }

        private void CathPoint()
        {
            if (!trigerToPullUp)
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
            hc.transform.position = Vector3.Lerp(startPos, endPos, current);           
            current += Time.deltaTime / (hc.timePullUpHook * normalazedPercentOfMaxDistance);
            if (current < 1)
            {
                hc.hook.position = endPos;
            }
            if (Vector3.Distance(hc.capturedTarget.transform.position, hc.transform.position) < 0.1f)
            {
                hc.transform.position = endPos;
                hc.hook.transform.position = hc.direction.normalized * hc.idleDistanseHook + hc.transform.position;

                hc.SetBehaviorRotation();
            }
        }

        private void Forward()
        {
            hc.hook.position = Vector3.Lerp(startPos, endPos, current);
            current += Time.deltaTime / (hc.timeThrowHook * normalazedPercentOfMaxDistance);
            if (current >= 1)
            {
                hc.hook.position = endPos;
                trigerToPullUp = true;
                startPos = hc.transform.position;
                endPos = hc.capturedTarget.transform.position;                
                current = 0;
            }
        }
    }
}
