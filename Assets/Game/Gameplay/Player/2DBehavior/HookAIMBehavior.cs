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
            Debug.Log("Start AIM state");
            // вычисляем угол между лучами
            float step = (hc.triggerAngleAIM * 2) / (hc.countRaiAIM - 1);
            // находим половину без остатка
            int halfCount = hc.countRaiAIM / 2;
            // переводим вектор направления в угол направления
            var angleDir = AccessoryMetods.GetAngleFromVector(hc.direction);
            // смещаем угол в одну сторону и в последствии юужем прибавлять \|/
            angleDir = angleDir - halfCount * step;
            RaycastHit2D[] hit = new RaycastHit2D[hc.countRaiAIM];

            //var dir = hc.direction;
            // стреляем лучами если находится враг то сразу переходим в состояние взаимодействия с врагом
            hit = RaiCast(hit, angleDir, step);
            //если врага нет, то проверяем на наличие точек и если она есть,
            //то переходим в состояние взаимодействия с точкой. Так сделано так, потому что враг в приоритете

            if (CheckingForEnemyOrProjectile(hit))
            {
                Debug.Log("Переход в захват врага или пули");
                hc.SetBehaviorCatchEnemyAndProjectile();
            }
            else if (CheckingforPoint(hit))
            {
                Debug.Log("Переход в захват точки");
                hc.SetBehaviorCarchPoint();
            }
            else
            {
                //если и точек нет то переходим в состояние пустого броска
                Debug.Log("Переход в пустой");
                hc.SetBehaviorCatchEmpty();
            }
        }

        private bool CheckingforPoint(RaycastHit2D[] hit)
        {
            bool check = false;
            foreach (var h in hit)
            {
                if ((h.collider != null) && (h.collider.gameObject.CompareTag("PointToCatch")))
                {
                    check = true;
                    hc.capturedTarget = h.collider.gameObject;
                    break;
                }
            }
            return check;
        }

        private bool CheckingForEnemyOrProjectile(RaycastHit2D[] hit)
        {
            bool check = false;
            foreach (var h in hit)
            {
                if ((h.collider != null) && (h.collider.gameObject.CompareTag("Enemy")))
                {
                    check = true;
                    hc.capturedTarget = h.collider.gameObject;
                    break;
                }
            }
            return check;
        }

        private RaycastHit2D[] RaiCast(RaycastHit2D[] hit, float angleDir, float step)
        {
            var dir = AccessoryMetods.GetVectorFromAngle(angleDir);

            for (int i = 0; i < hc.countRaiAIM; i++)
            {
                Debug.DrawRay(hc.pointToRaiCast.position, dir * hc.maxDistanseHook, Color.red, 1);
                hit[i] = Physics2D.Raycast(
                    hc.pointToRaiCast.position,
                    dir,
                    hc.maxDistanseHook,
                    hc.layerToTouchAIM);
                dir = AccessoryMetods.GetVectorFromAngle(angleDir + step * (i + 1));
            }
            return hit;
        }

        public void Exit()
        {
            Debug.Log("Exit AIM state");
        }

        public void UpdateBehavior()
        {
            // Debug.Log("Update AIM state");
        }
    }
}