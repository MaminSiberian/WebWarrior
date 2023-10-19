using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public class MiniChaser : EnemyBase, IDamagable, IGrabable
    {
        protected float chasingSpeed;
        protected float attackingSpeed;
        protected float chasingDistance;
        protected float attackingDistance;

        #region MONOBEHS
        private void Start()
        {
            StartIdle();
        }
        private void FixedUpdate()
        {
            if (state == State.Released && rb.velocity == Vector3.zero)
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
            chasingSpeed = data.miniChaserChasingDistance;
            attackingSpeed = data.miniChaserAttackingSpeed;
            chasingDistance = data.miniChaserChasingDistance;
            attackingDistance = data.miniChaserAttackingDistance;
        }

        #region GRAB
        public void OnGrab()
        {
            state = State.Grabbed;
        }

        public void OnRelease()
        {
            state = State.Released;
        }
        #endregion
    }
}
