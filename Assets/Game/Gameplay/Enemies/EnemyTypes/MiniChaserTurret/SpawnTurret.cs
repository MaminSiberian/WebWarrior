using UnityEngine;

namespace Enemies
{
    public class SpawnTurret : Turret
    {
        protected override void StartAttacking()
        {
            state = State.Attacking;
            var chaser = MiniChaserPool.GetMiniChaser();
            Physics.IgnoreCollision(coll, chaser.GetComponent<Collider>());
            chaser.transform.position = transform.position + Vector3.forward * 1f;
            OnAttackEnded();
        }
        protected override void SetData()
        {
            base.SetData();
            patrollingSpeed = data.spawnTurretPatrollingSpeed;
            attackingDistance = data.spawnTurretAttackingDistance;
            reloadingTime = data.spawnTurretReloadingTime;
        }
    }
}
