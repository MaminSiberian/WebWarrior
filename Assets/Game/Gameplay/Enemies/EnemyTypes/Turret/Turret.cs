using NaughtyAttributes;
using UnityEngine;

namespace Enemies
{
    public class Turret : EnemyBase
    {
        [SerializeField] private bool shootPlayer = true;
        [SerializeField, HideIf("shootPlayer")] private Transform _aimTarget;

        protected float patrollingSpeed;
        protected float attackingForce;
        protected float attackingDistance;
        protected float reloadingTime;

        protected Collider coll;
        private Transform aimTarget;
        private bool readyToFire = true;
        private float reloadingTimer = 0f;

        #region MONOBEHS
        protected override void Awake()
        {
            base.Awake();
            coll = GetComponent<Collider>();
            aimTarget = shootPlayer ? player : _aimTarget;
        }
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
            if (!readyToFire && state != State.Reloading)
            {
                StartReloading();
            }

            if (shootPlayer 
                && DistanceToPlayer() <= attackingDistance
                && PlayerIsVisible()
                && state != State.Attacking
                && readyToFire)
            {
                StartAttacking();
            }
            if (!shootPlayer 
                && state != State.Attacking
                && readyToFire)
            {
                StartAttacking();
            }

            if ((state == State.Patrolling 
                || state == State.Attacking
                || state == State.Reloading)
                && target != null)
            {
                MoveEnemy();
                CheckPatrollingPoints();
                return;
            }
            if (state == State.Reloading) return;
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
        protected virtual void StartAttacking()
        {
            state = State.Attacking;
            var proj = ProjectilePool.GetProjectile();
            proj.TurnOffCollision(coll);
            proj.transform.position = transform.position;
            proj.GetComponent<Rigidbody>()
                .AddForce((aimTarget.position - transform.position).normalized * attackingForce);
            OnAttackEnded();
        }
        protected void OnAttackEnded()
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

        protected override void SetData()
        {
            base.SetData();
            patrollingSpeed = data.turretPatrollingSpeed;
            attackingForce = data.turretAttackingForce;
            attackingDistance = data.turretAttackingDistance;
            reloadingTime = data.turretReloadingTime;
        }

        protected override void OnEnemyDeath()
        {
            Deactivate();
        }
    }
}
