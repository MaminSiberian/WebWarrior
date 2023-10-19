using UnityEngine;

namespace Enemies
{
    public class Shooter : EnemyBase, IDamagable, IGrabable
    {
        private float patrollingSpeed;
        private float attackingForce;
        private float attackingDistance;
        private float reloadingTime;

        private Collider coll;
        private bool readyToFire = true;
        private float reloadingTimer = 0f;

        #region MONOBEHS
        protected override void Awake()
        {
            base.Awake();
            coll = GetComponent<Collider>();
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
            if (state == State.Released && rb.velocity == Vector3.zero)
            {
                StartPatrolling();
                return;
            }

            if (state == State.Attacking 
                || state == State.Death 
                || state == State.Reloading
                || state == State.Grabbed
                || state == State.Released) return;

            if (!readyToFire && state != State.Reloading)
            {
                StartReloading();
                return;
            }

            if (DistanceToPlayer() <= attackingDistance
                && PlayerIsVisible()
                && state != State.Attacking)
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
            var proj = ProjectilePool.GetProjectile();
            proj.TurnOffCollision(coll);
            proj.transform.position = transform.position;
            proj.GetComponent<Rigidbody>()
                .AddForce((player.position - transform.position).normalized * attackingForce);
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
            Debug.Log(name + " damaged");
            //OnEnemyDeath();
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
