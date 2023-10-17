using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : PoolableObject
    {
        public State state {  get; protected set; }

        protected Rigidbody rb;
        protected Vector3 target;
        protected float moveSpeed;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        protected virtual void MoveEnemy()
        {
            rb.MovePosition(transform.position + (target - transform.position).normalized * moveSpeed * Time.fixedDeltaTime);
        }
        protected abstract void OnEnemyDeath();
    }
}
