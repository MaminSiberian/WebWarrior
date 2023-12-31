using HookControl;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : PoolableObject
    {
        [SerializeField] protected List<Transform> patrolPoints;

        protected LayerMask layerForPlayerIsVisible;
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
            player = FindAnyObjectByType<HookController>().transform;
            //player = FindAnyObjectByType<TestPlayer>().transform;
            Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
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
        protected bool PlayerIsVisible()
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, player.position, out hit, layerForPlayerIsVisible))
            {
                if (hit.collider.gameObject.layer == Layers.walls )
                    //|| hit.collider.gameObject.layer == Layers.magnit 
                    //|| hit.collider.gameObject.layer == Layers.thorns)
                    return false;
            }
            return true;
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
        protected virtual void SetData()
        {
            layerForPlayerIsVisible = data.LayerForPlayerIsVisible;
        }
        #endregion
    }
}
