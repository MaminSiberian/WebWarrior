using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookCatcEmptyhBehavior : IHookBehavior
    {
        private HookController hc;
        //private Vector3 startPos;
        //private Vector3 endPos;
        //private float current;
        //private float normalazedPercentOfMaxDistance; //coefficient for distance and time
        //private bool trigerToPullUp = false; // separates the throw and the attraction

        public HookCatcEmptyhBehavior(HookController hookController)
        {
            this.hc = hookController;
        }

        public void Enter()
        {
            //Debug.Log("Enter CathEmpty state");
            hc.trigerToPullUp = false;
            //checking for the presence of a wall
            var hit = CheckWall();
            //if there is a wall, then we calculate the normalized percentage of the maximum distance, if not, then 1
            hc.normalazedPercentOfMaxDistance = hit.collider != null ?
                AccessoryMetods.NormalizedPercentOfDistanceXZ(hit.point, hc.maxDistanseHook, hc.transform.position) : 1f;
            
            hc.startPos = hc.defaultPointHook.position;
            hc.endPos = hc.direction.normalized * hc.maxDistanseHook * hc.normalazedPercentOfMaxDistance + hc.transform.position;
            hc.current = 0;
        }



        public void Exit()
        {
            //Debug.Log("Exit CathEmpty state");
            hc.isEndHook = true;
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
            hc.hook.position = Vector3.Lerp(hc.startPos, hc.endPos, hc.current);
            hc.current += Time.deltaTime / (hc.timePullUpHook * hc.normalazedPercentOfMaxDistance);
            if (hc.current >= 1)
            {
                hc.hook.position = hc.endPos;
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
                hc.startPos = hc.endPos;
                hc.endPos = hc.defaultPointHook.position;
                hc.current = 0;
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