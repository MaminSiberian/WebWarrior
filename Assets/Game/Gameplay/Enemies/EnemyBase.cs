using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : PoolableObject
    {
        [SerializeField] protected List<Transform> patrolPoints;

        protected float patrDelta = 0.1f;
        protected int currentPatrolPos = 0;

        public State state {  get; protected set; }

        protected EnemyData data;
        protected Transform player;
        protected Rigidbody rb;
        protected Transform target;
        protected float moveSpeed;

        #region MONOBEHS
        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            data = FindAnyObjectByType<EnemyData>();
            player = FindAnyObjectByType<TestPlayer>().transform;
            SetData();
        }
        protected virtual void OnEnable()
        {
            data.OnDataChanged += SetData;
        }
        protected virtual void OnDisable()
        {
            data.OnDataChanged -= SetData;
        }
        #endregion

        protected virtual void MoveEnemy()
        {
            rb.MovePosition(transform.position + (target.position - transform.position).normalized * moveSpeed * Time.fixedDeltaTime);
        }
        protected float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.position);
        }

        #region PATROLLING
        protected void CheckPatrollingPoints()
        {
            if (Vector3.Distance(transform.position, target.position) <= patrDelta)
                SwitchPatrollingPoint();
        }
        protected void SwitchPatrollingPoint()
        {
            if (currentPatrolPos == patrolPoints.Count - 1)
                currentPatrolPos = 0;
            else
                currentPatrolPos++;

            target = patrolPoints[currentPatrolPos];
        }
        #endregion

        #region ABSTRACTS
        protected abstract void OnEnemyDeath();
        protected abstract void SetData();
        #endregion
    }
}
