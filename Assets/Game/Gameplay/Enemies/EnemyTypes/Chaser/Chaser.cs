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
        private Tween tween;

        #region MONOBEHS
        protected override void OnDisable()
        {
            base.OnDisable();
            tween.Kill();
        }
        private void Start()
        {
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
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.layer == Layers.walls)
            {
                StartPatrolling();
            }
        }
        #endregion

        #region CHANGING_STATES
        private void StartIdle()
        {
            layersToDamage = new List<int>() { playerLayer };
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

            layersToDamage = new List<int>() { playerLayer };
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
            tween = transform.DOMove(target.position, attackingSpeed)
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
            chasingSpeed = Random.Range(data.chaserMinChasingSpeed, data.chaserMaxChasingSpeed);
            attackingSpeed = data.chaserAttackingSpeed;
            chasingDistance = data.chaserChasingDistance;
            attackingDistance = data.chaserAttackingDistance;
        }

        #region GRAB
        public void OnGrab()
        {
            layersToDamage = new List<int>() { defaultLayer };
            state = State.Grabbed;
            tween.Kill();
        }

        public void OnRelease()
        {
            layersToDamage = new List<int>() { enemyLayer };
            state = State.Released;
        }

        #endregion
        public void OnDamage()
        {
            GetDamage();
        }
    }
}