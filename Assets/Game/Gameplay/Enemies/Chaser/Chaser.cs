using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Chaser : EnemyBase, IDamagable, IGrabable
    {
        private float patrollingSpeed;
        private float chasingSpeed;
        private float attackingSpeed;
        private float chasingDistance;
        private float attackingDistance;

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
        private void FixedUpdate()
        {
            Debug.Log(DistanceToPlayer());
            if (DistanceToPlayer() <= attackingDistance)
            {

            }
            if (DistanceToPlayer() <= chasingDistance)
            {
                StartChasing();
                return;
            }

            if (state == State.Patrolling || state == State.Chasing)
            {
                MoveEnemy();
                CheckPatrollingPoints();
            }
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
        private void StartChasing()
        {
            state = State.Chasing;
            moveSpeed = chasingSpeed;
            target = player;
        }
        private void StartAttacking()
        {
            state = State.Attacking;         
            target = player;
        }
        private void OnAttackEnded()
        {
            StartIdle();
        }
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
            throw new System.NotImplementedException();
        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
