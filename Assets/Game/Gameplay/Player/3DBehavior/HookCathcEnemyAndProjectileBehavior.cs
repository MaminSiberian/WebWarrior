using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookCathcEnemyAndProjectileBehavior : IHookBehavior
    {
        private HookController hc;
        //private Vector3 startPos;
        //private Vector3 endPos;
        //private float current;
        //private float normalazedPercentOfMaxDistance; // коэффициент для дистанции и времени
        //private bool trigerToPullUp = false; // разделяет бросок и притяжение

        public HookCathcEnemyAndProjectileBehavior(HookController hookController)
        {
            this.hc = hookController;
        }

        public void Enter()
        {
            //Debug.Log("Enter CatchEnemyAndProjectile state");
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
            //Debug.Log("Exit CatchEnemyAndProjectile state");
            hc.isEndHook = true;
            hc.hook.position = hc.defaultPointHook.position;
        }

        public void UpdateBehavior()
        {
            Cath();
        }

        private void Cath()
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
            hc.icCaptureSomthing = true;
            hc.hook.position = Vector3.Lerp(hc.startPos, hc.endPos, hc.current);
            hc.current += Time.deltaTime / (hc.timePullUpHook * hc.normalazedPercentOfMaxDistance);
            hc.capturedTarget.transform.position = hc.hook.transform.position;

            if (hc.current >= 1)
            {
                //hc.hook.position = endPos;
                hc.SetBehaviorRotationWithObject();
            }
        }

        private void Forward()
        {
            EventSystem.SendThrowHook();
            hc.hook.position = Vector3.Lerp(hc.startPos, hc.endPos, hc.current);
            hc.current += Time.deltaTime / (hc.timeThrowHook * hc.normalazedPercentOfMaxDistance);
            if (hc.current >= 1)
            {
                hc.grabableTarget.OnGrab();
                //hc.capturedTarget.GetComponent<IGrabable>()?.OnGrab();
                hc.hook.position = hc.endPos;
                hc.trigerToPullUp = true;
                hc.startPos = hc.endPos;
                hc.endPos = hc.defaultPointHook.position;
                hc.current = 0;
            }
        }
    }
}
