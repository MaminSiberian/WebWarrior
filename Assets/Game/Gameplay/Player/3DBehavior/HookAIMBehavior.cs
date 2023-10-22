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
            // ��������� ���� ����� ������
            float step = (hc.triggerAngleAIM * 2) / (hc.countRaiAIM - 1);
            // ������� �������� ��� �������
            int halfCount = hc.countRaiAIM / 2;
            // ��������� ������ ����������� � ���� �����������
            var angleDir = AccessoryMetods.GetAngleFromVectorXZ(hc.direction);
            // ������� ���� � ���� ������� � � ����������� ����� ���������� \|/
            angleDir = angleDir - halfCount * step;
            RaycastHit[] hit = new RaycastHit[hc.countRaiAIM];

            //var dir = hc.direction;
            // �������� ������ ���� ��������� ���� �� ����� ��������� � ��������� �������������� � ������
            hit = RaiCast(hit, angleDir, step);
            //���� ����� ���, �� ��������� �� ������� ����� � ���� ��� ����,
            //�� ��������� � ��������� �������������� � ������. ��� ������� ���, ������ ��� ���� � ����������

            if (CheckingForEnemyOrProjectile(hit))
            {
                Debug.Log("������� � ������ ����� ��� ����");
                hc.SetBehaviorCatchEnemyAndProjectile();
            }
            else if (CheckingforPoint(hit))
            {
                Debug.Log("������� � ������ �����");
                hc.SetBehaviorCarchPoint();
            }
            else
            {
                //���� � ����� ��� �� ��������� � ��������� ������� ������
                Debug.Log("������� � ������");
                hc.SetBehaviorCatchEmpty();
            }
        }

        private bool CheckingforPoint(RaycastHit[] hit)
        {
            bool check = false;
            foreach (var h in hit)
            {
                if ((h.collider != null) && (h.collider.gameObject.CompareTag("PointToCatch")))
                {
                    Debug.Log(1);
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
                Debug.Log("���� ��� �������������");
                if ((h.collider != null) && (h.collider.gameObject.CompareTag("Enemy")))
                {
                    check = true;
                    hc.capturedTarget = h.collider.gameObject;
                    break;
                }
            }
            return check;
        }

        private RaycastHit[] RaiCast(RaycastHit[] hit, float angleDir, float step)
        {
            Vector3 dir = AccessoryMetods.GetVectorFromAngleXZ(angleDir);
            //Debug.Log(dir);

            for (int i = 0; i < hc.countRaiAIM; i++)
            {
                Debug.DrawRay(hc.pointToRaiCast.position, dir * hc.maxDistanseHook, Color.red, 1);
                Physics.Raycast(
                    hc.pointToRaiCast.position,
                    dir,
                    out hit[i],
                    hc.maxDistanseHook,
                    hc.layerToTouchAIM);
                dir = AccessoryMetods.GetVectorFromAngleXZ(angleDir + step * (i + 1));
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