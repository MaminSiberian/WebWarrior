using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemies
{
    public class Shooter : EnemyBase, IDamagable, IGrabable, IDamager
    {
        private float patrollingSpeed;
        private float attackingForce;
        private float attackingDistance;
        private float reloadingTime;

        private Collider coll;
        private bool readyToFire = true;
        private float reloadingTimer = 0f;

        public List<int> layersToDamage { get; protected set; }
        private int defaultLayer = Layers.defaultLayer;
        private int playerLayer = Layers.player;
        private int enemyLayer = Layers.enemy;

        #region MONOBEHS
        protected override void Awake()
        {
            base.Awake();
            coll = GetComponent<Collider>();
            layersToDamage = new List<int>() { playerLayer };
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
            if (state == State.Released && DistanceToPlayer() >= attackingDistance * 2)
            {
                StartIdle();
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
            Debug.Log("Grab");
            layersToDamage = new List<int>() { defaultLayer };
            state = State.Grabbed;
        }

        public void OnRelease()
        {
            Debug.Log("Release");
            layersToDamage = new List<int>() { enemyLayer };
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

        public void OnDamage()
        {
            if (layersToDamage.Any(l => l == enemyLayer))
                GetDamage();
        }
    }
}
