using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public class Shooter : EnemyBase, IDamagable, IGrabable
    {
        private float patrollingSpeed;
        private float attackingForce;
        private float attackingDistance;
        private float reloadingTime;

        private bool readyToFire = true;
        private float reloadingTimer = 0f;

        #region MONOBEHS
        private void Start()
        {
            StartPatrolling();
        }
        protected override void Update()
        {
            base.Update();

            if (state == State.Reloading)
            {
                Reload();
                return;
            }
        }
        private void FixedUpdate()
        {
            
            if (state == State.Attacking || 
                state == State.Death || 
                state == State.Reloading) return;

            if (!readyToFire && state != State.Reloading)
            {
                StartReloading();
                return;
            }

            if (DistanceToPlayer() <= attackingDistance && state != State.Attacking)
            {
                StartAttacking();
                return;
            }

            if (state == State.Patrolling)
            {
                MoveEnemy();
                CheckPatrollingPoints();
                return;
            }
            StartPatrolling();
        }
        #endregion

        #region CHANGING_STATES
        private void StartIdle()
        {
            state = State.Idle;
        }
        private void StartPatrolling()
        {
            if (patrolPoints.Count == 0)
            {
                StartIdle();
                return;
            }

            state = State.Patrolling;
            moveSpeed = patrollingSpeed;
            target = patrolPoints[currentPatrolPos];
        }

        #region ATTACKING
        private void StartAttacking()
        {
            state = State.Attacking;
            target = player;
            var proj = ProjectilePool.GetProjectile();
            proj.transform.position = transform.position;
            proj.GetComponent<Rigidbody>()
                .AddForce((target.position - transform.position).normalized * attackingForce);
            OnAttackEnded();
        }
        private void OnAttackEnded()
        {
            readyToFire = false;
            StartReloading();
        }
        #endregion

        #region RELOADING
        private void StartReloading()
        {
            state = State.Reloading;
        }
        private void Reload()
        {
            if (reloadingTimer >= reloadingTime)
            {
                reloadingTimer -= reloadingTime;
                OnReloadEnded();
            }
            else
                reloadingTimer += Time.deltaTime;
        }
        private void OnReloadEnded()
        {
            readyToFire = true;
            StartPatrolling();
        }
        #endregion

        #endregion

        public void GetDamage()
        {
            OnEnemyDeath();
        }

        public void OnGrab()
        {
            throw new System.NotImplementedException();
        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnEnemyDeath()
        {
            state = State.Death;
            Deactivate();
        }

        protected override void SetData()
        {
            patrollingSpeed = data.shooterPatrollingSpeed;
            attackingForce = data.shooterAttackingForce;
            attackingDistance = data.shooterAttackingDistance;
            reloadingTime = data.shooterReloadingTime;
        }
    }
}
