using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

namespace Enemies
{
    public class Chaser : EnemyBase, IDamagable, IGrabable, IDamager
    {
        protected float patrollingSpeed;
        protected float chasingSpeed;
        protected float attackingSpeed;
        protected float chasingDistance;
        protected float attackingDistance;

        public List<int> layersToDamage { get; protected set; }
        private int defaultLayer = Layers.defaultLayer;
        private int playerLayer = Layers.player;
        private int enemyLayer = Layers.enemy;

        #region MONOBEHS
        private void Start()
        {
            layersToDamage = new List<int>() { playerLayer };
            StartPatrolling();
        }
        private void FixedUpdate()
        {
            if (state == State.Released && DistanceToPlayer() >= chasingDistance * 2)
            {
                StartIdle();
                return;
            }

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
            rb.velocity = Vector3.zero;
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
            Debug.Log(name + " damaged");
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
            layersToDamage = new List<int>() { defaultLayer };
            state = State.Grabbed;
        }

        public void OnRelease()
        {
            layersToDamage = new List<int>() { enemyLayer };
            state = State.Released;
        }

        public void OnDamage()
        {
            if (layersToDamage.Any(l => l == enemyLayer))
                GetDamage();
        }
        #endregion
    }
}