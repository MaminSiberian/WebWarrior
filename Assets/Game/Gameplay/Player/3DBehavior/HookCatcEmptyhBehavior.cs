using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookCatcEmptyhBehavior : IHookBehavior
    {
        private HookController hc;
        private Vector3 startPos;
        private Vector3 endPos;
        private float current;
        private float normalazedPercentOfMaxDistance; // коэффициент для дистанции и времени
        private bool trigerToPullUp = false; // разделяет бросок и притяжение

        public HookCatcEmptyhBehavior(HookController hookController)
        {
            this.hc = hookController;
        }

        public void Enter()
        {
            Debug.Log("Enter CathEmpty state");
            trigerToPullUp = false;
            //проверка на наличии стены
            var hit = CheckWall();
            //если стена есть то выссчитываем номарлизованный процент от максимального расстояния, если нет то 1
            normalazedPercentOfMaxDistance = hit.collider != null ?
                AccessoryMetods.NormalizedPercentOfDistanceXZ(hit.point, hc.maxDistanseHook, hc.transform.position) : 1f;
            
            startPos = hc.direction.normalized * hc.idleDistanseHook + hc.transform.position;
            endPos = hc.direction.normalized * hc.maxDistanseHook * normalazedPercentOfMaxDistance + hc.transform.position;
            current = 0;
        }



        public void Exit()
        {
            Debug.Log("Exit CathEmpty state");
            hc.isEndHook = true;
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
            hc.hook.position = Vector3.Lerp(startPos, endPos, current);
            current += Time.deltaTime / (hc.timePullUpHook * normalazedPercentOfMaxDistance);
            if (current >= 1)
            {
                hc.hook.position = endPos;
                hc.SetBehaviorRotation();
            }
        }

        private void Forward()
        {
            EventSystem.SendThrowHook();
            hc.hook.position = Vector3.Lerp(startPos, endPos, current);
            current += Time.deltaTime / (hc.timeThrowHook * normalazedPercentOfMaxDistance);
            if (current >= 1)
            {
                hc.hook.position = endPos;
                trigerToPullUp = true;
                startPos = endPos;
                endPos = hc.direction.normalized * hc.idleDistanseHook + hc.transform.position;
                current = 0;
            }
        }

        private RaycastHit CheckWall()
        {
             Physics.Raycast(hc.pointToRaiCast.position, hc.direction, out RaycastHit hit,
                hc.maxDistanseHook, hc.layerWall);
            return hit;
        }
    }
}