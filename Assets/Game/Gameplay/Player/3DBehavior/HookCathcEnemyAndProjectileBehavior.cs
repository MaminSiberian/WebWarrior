using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookCathcEnemyAndProjectileBehavior : IHookBehavior
    {
        private HookController hc;
        private Vector3 startPos;
        private Vector3 endPos;
        private float current;
        private float normalazedPercentOfMaxDistance; // коэффициент для дистанции и времени
        private bool trigerToPullUp = false; // разделяет бросок и притяжение

        public HookCathcEnemyAndProjectileBehavior(HookController hookController)
        {
            this.hc = hookController;
        }

        public void Enter()
        {
            Debug.Log("Enter CatchEnemyAndProjectile state");
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
            Debug.Log("Exit CatchEnemyAndProjectile state");
            hc.isEndHook = true;
            hc.hook.position = hc.defaultPointHook.position;
        }

        public void UpdateBehavior()
        {
            Cath();
        }

        private void Cath()
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
            EventSystem.SendPullBackHook();
            hc.icCaptureSomthing = true;
            hc.hook.position = Vector3.Lerp(startPos, endPos, current);
            current += Time.deltaTime / (hc.timePullUpHook * normalazedPercentOfMaxDistance);
            hc.capturedTarget.transform.position = hc.hook.transform.position;

            if (current >= 1)
            {
                //hc.hook.position = endPos;
                hc.SetBehaviorRotationWithObject();
            }
        }

        private void Forward()
        {
            EventSystem.SendThrowHook();
            hc.hook.position = Vector3.Lerp(startPos, endPos, current);
            current += Time.deltaTime / (hc.timeThrowHook * normalazedPercentOfMaxDistance);
            if (current >= 1)
            {
                hc.capturedTarget.GetComponent<IGrabable>()?.OnGrab();
                hc.hook.position = endPos;
                trigerToPullUp = true;
                startPos = endPos;
                endPos = hc.direction.normalized * hc.idleDistanseHook + hc.transform.position;
                current = 0;
            }
        }
    }
}
