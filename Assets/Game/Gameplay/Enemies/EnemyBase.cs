using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : PoolableObject
    {
        public State state {  get; protected set; }

        protected EnemyData data;
        protected Rigidbody rb;
        protected Vector3 target;
        protected float moveSpeed;


        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            data = FindAnyObjectByType<EnemyData>();
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
        protected virtual void MoveEnemy()
        {
            rb.MovePosition(transform.position + (target - transform.position).normalized * moveSpeed * Time.fixedDeltaTime);
        }
        protected abstract void OnEnemyDeath();
        protected abstract void SetData();
    }
}
