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

        #region MONOBEHS
        private void Start()
        {
            if (patrolPoints.Count != 0)
            {
                StartPatrolling();
            }
            else
                state = State.Idle;
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
            if (patrolPoints.Count != 0)
            {
                StartPatrolling();
            }
            else
                StartIdle();
        }
        #endregion

        #region CHANGING_STATES
        private void StartIdle()
        {
            state = State.Idle;
        }
        private void StartPatrolling()
        {
            state = State.Patrolling;
            moveSpeed = patrollingSpeed;
            target = patrolPoints[currentPatrolPos];
        }

        private void StartAttacking()
        {
            
        }
        private void OnAttackEnded()
        {
            
        }
        private void StartReloading()
        {

        }
        private void Reload()
        {

        }
        private void OnReloadEnded()
        {

        }
        #endregion
        
        public void GetDamage()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
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
