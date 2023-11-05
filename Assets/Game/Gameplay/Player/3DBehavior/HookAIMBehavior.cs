using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookAIMBehavior : IHookBehavior
    {
        private HookController hc;

        public HookAIMBehavior(HookController hc)
        {
            this.hc = hc;
        }

        public void Enter()
        {
            //Debug.Log("Start AIM state");
            // calculate the angle between the rays
            float step = (hc.triggerAngleAIM * 2) / (hc.countRaiAIM - 1);
            // find half without remainder
            int halfCount = hc.countRaiAIM / 2;
            // converts a direction vector to a direction angle
            var angleDir = AccessoryMetods.GetAngleFromVectorXZ(hc.direction);
            // shifts the angle to one side and subsequently we will add \|/
            //angleDir = angleDir - halfCount * step;
            RaycastHit[] hit = new RaycastHit[hc.countRaiAIM];
            hc.rb.velocity = Vector3.zero;

            //var dir = hc.direction;
            //  
            hit = RaiCast(hit, angleDir, step, hc.layerForEnemyToTouchAIM);

            if (CheckingForEnemyOrProjectile(hit))
            {
                //Debug.Log("Переход в захват врага или пули");
                hc.SetBehaviorCatchEnemyAndProjectile();
                return;
            }

            hit = RaiCast(hit, angleDir, step, hc.layerForPlatformToTouchAIM);

            if (CheckingforPoint(hit))
            {
                //Debug.Log("Переход в захват точки");
                hc.SetBehaviorCarchPoint();
                return;
            }
            //else
            //{
                //если и точек нет то переходим в состояние пустого броска
                //Debug.Log("Переход в пустой");
                hc.SetBehaviorCatchEmpty();
           // }
        }

        private bool CheckingforPoint(RaycastHit[] hit)
        {
            bool check = false;
            foreach (var h in hit)
            {
                if ((h.collider != null) && (h.collider.gameObject.CompareTag("Platform")))
                {
                    check = true;
                    hc.capturedTarget = h.collider.gameObject;
                    break;
                }
            }
            return check;
        }

        private bool CheckingForEnemyOrProjectile(RaycastHit[] hit)
        {
            bool check = false;
            foreach (var h in hit)
            {
                if ((h.collider != null) && (h.collider.gameObject.GetComponent<IGrabable>() != null))
                {
                    hc.grabableTarget = h.collider.gameObject.GetComponent<IGrabable>();
                    check = true;
                    hc.capturedTarget = h.collider.gameObject;
                    break;
                }
            }
            return check;
        }

        private RaycastHit[] RaiCast(RaycastHit[] hit, float angleDir, float step, LayerMask layer)
        {

            Vector3 dir = AccessoryMetods.GetVectorFromAngleXZ(angleDir);
            int i = 0;
            var c = Color.white;
            while (i < hc.countRaiAIM)
            {
                Debug.DrawRay(hc.pointToRaiCast.position, dir * hc.maxDistanseHook, c, 1);
                Physics.Raycast(
                    hc.pointToRaiCast.position,
                    dir,
                    out hit[i],
                    hc.maxDistanseHook,
                    layer);
                    //hc.layerToTouchAIM);
                i++;
                if (i % 2 == 0)
                {
                    c = Color.black;
                    dir = AccessoryMetods.GetVectorFromAngleXZ(angleDir + step * (i - 1));
                }
                else
                {
                    c = Color.red;
                    dir = AccessoryMetods.GetVectorFromAngleXZ(angleDir + step * (-i));
                }
            }
            return hit;
        }

        public void Exit()
        {
            // Debug.Log("Exit AIM state");
        }

        public void UpdateBehavior()
        {
            // Debug.Log("Update AIM state");
        }
    }
}