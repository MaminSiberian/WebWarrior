using NaughtyAttributes;
using UnityEngine;

namespace Enemies
{
    public class Carrier : Chaser
    {
        [Button]
        protected override void OnEnemyDeath()
        {
            var chaser1 = MiniChaserPool.GetMiniChaser();
            chaser1.transform.position = transform.position + Vector3.left * 0.5f;

            var chaser2 = MiniChaserPool.GetMiniChaser();
            chaser1.transform.position = transform.position + Vector3.right * 0.5f;

            state = State.Death;
            Deactivate();
        }
        protected override void SetData()
        {
            patrollingSpeed = data.carrierPatrollingSpeed;
            chasingSpeed = data.carrierChasingSpeed;
            attackingSpeed = data.carrierAttackingSpeed;
            chasingDistance = data.carrierChasingDistance;
            attackingDistance = data.carrierAttackingDistance;
        }
    }
}
