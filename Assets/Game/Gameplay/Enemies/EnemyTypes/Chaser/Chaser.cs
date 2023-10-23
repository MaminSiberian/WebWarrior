using UnityEngine;
using DG.Tweening;

namespace Enemies
{
    public class Chaser : EnemyBase, IDamagable, IGrabable
    {
        protected float patrollingSpeed;
        protected float chasingSpeed;
        protected float attackingSpeed;
        protected float chasingDistance;
        protected float attackingDistance;

        #region MONOBEHS
        private void Start()
        {
            StartPatrolling();
        }
        private void FixedUpdate()
        {
            /*if (state == State.Released && rb.velocity == Vector3.zero)
            {
                Debug.Log("Here");
                StartPatrolling();
                return;
            }*/

            if (state == State.Attacking 
                || state == State.Death
                || state == State.Grabbed
                || state == State.Released) return;

            if (DistanceToPlayer() <= attackingDistance 
                && PlayerIsVisible() 
                && state != State.Attacking)
            {
                StartAttacking();
                return;
            }
            if (DistanceToPlayer() <= chasingDistance 
                && PlayerIsVisible()
                && state != State.Chasing)
            {
                StartChasing();
                return;
            }

            if (state == State.Chasing)
            {
                if (DistanceToPlayer() > chasingDistance || !PlayerIsVisible())
                    StartIdle();
                else
                    MoveEnemy();
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
        private void StartChasing()
        {
            state = State.Chasing;
            moveSpeed = chasingSpeed;
            target = player;
        }

        #region ATTACKING
        private void StartAttacking()
        {
            state = State.Attacking;         
            target = player;
            transform.DOMove(target.position, attackingSpeed)
                .SetEase(Ease.InBack)
                .SetSpeedBased()
                .OnKill(() => OnAttackEnded());
        }
        private void OnAttackEnded()
        {
            StartIdle();
        }
        #endregion

        #endregion

        public void GetDamage()
        {
            OnEnemyDeath();
        }
        protected override void OnEnemyDeath()
        {
            state = State.Death;
            Deactivate();
        }
        protected override void SetData()
        {
            patrollingSpeed = data.chaserPatrollingSpeed;
            chasingSpeed = data.chaserChasingSpeed;
            attackingSpeed = data.chaserAttackingSpeed;
            chasingDistance = data.chaserChasingDistance;
            attackingDistance = data.chaserAttackingDistance;
        }

        #region GRAB
        public void OnGrab()
        {
            Debug.Log("Grab");
            state = State.Grabbed;
        }

        public void OnRelease()
        {
            Debug.Log("Release");
            state = State.Released;
        }
        #endregion
    }
}
