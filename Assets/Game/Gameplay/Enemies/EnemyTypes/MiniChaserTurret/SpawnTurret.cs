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
            chaser.transform.position = transform.position + Vector3.forward * 0.5f;
            OnAttackEnded();
        }
        protected override void SetData()
        {
            patrollingSpeed = data.spawnTurretPatrollingSpeed;
            attackingDistance = data.spawnTurretAttackingDistance;
            reloadingTime = data.spawnTurretReloadingTime;
        }
    }
}
