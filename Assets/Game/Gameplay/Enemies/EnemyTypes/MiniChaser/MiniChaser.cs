using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemies
{
    public class MiniChaser : EnemyBase, IDamagable, IGrabable, IDamager
    {
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
            StartIdle();
        }
        private void FixedUpdate()
        {
            if (state == State.Released && DistanceToPlayer() >= chasingDistance * 1.5f)
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

            StartIdle();
        }
        #endregion

        #region CHANGING_STATES
        private void StartIdle()
        {
            state = State.Idle;
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
            chasingSpeed = Random.Range(data.miniChaserMinChasingSpeed, data.miniChaserMaxChasingSpeed);
            attackingSpeed = data.miniChaserAttackingSpeed;
            chasingDistance = data.miniChaserChasingDistance;
            attackingDistance = data.miniChaserAttackingDistance;
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

        #endregion
        public void OnDamage()
        {
            GetDamage();
        }
    }
}
