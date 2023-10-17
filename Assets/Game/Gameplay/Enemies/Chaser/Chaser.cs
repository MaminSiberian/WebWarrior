using System.Collections.Generic;
using UnityEngine;

// Враг, атакующий в ближнем бою
namespace Enemies
{
    public class Chaser : EnemyBase, IDamagable, IGrabable
    {
        [SerializeField] private List<Transform> patrolPoints;

        private float patrDelta = 0.1f;
        protected int currentPatrolPos = 0;

        private void Start()
        {
            if (patrolPoints.Count != 0)
            {
                state = State.Patrolling;
                target = patrolPoints[currentPatrolPos].position;
            }
            else
                state = State.Idle;
        }

        private void FixedUpdate()
        {
            if (state == State.Patrolling)
            {
                MoveEnemy();
                CheckPatrollingPoints();
            }
        }

        public void GetDamage()
        {
            throw new System.NotImplementedException();
        }

        public void OnGrab()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnEnemyDeath()
        {
            throw new System.NotImplementedException();
        }
        private void CheckPatrollingPoints()
        {
            if (Vector3.Distance(transform.position, target) <= patrDelta)
                SwitchPatrollingPoint();
        }
        private void SwitchPatrollingPoint()
        {
            if (currentPatrolPos == patrolPoints.Count - 1)
                currentPatrolPos = 0;
            else
                currentPatrolPos++;

            target = patrolPoints[currentPatrolPos].position;
        }

        protected override void SetData()
        {
            moveSpeed = data.chaserSpeed;
        }
    }
}
