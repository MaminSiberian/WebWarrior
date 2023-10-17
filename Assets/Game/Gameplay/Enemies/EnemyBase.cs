using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : MonoBehaviour
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

        }
        protected abstract void OnEnemyDeath();
    }
}
