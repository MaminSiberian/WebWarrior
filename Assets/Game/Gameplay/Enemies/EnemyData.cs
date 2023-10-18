using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyData : MonoBehaviour
    {
        [Header("Chaser")]
        [SerializeField] private float _chaserPatrollingSpeed;
        [SerializeField] private float _chaserChasingSpeed;
        [SerializeField] private float _chaserAttackingSpeed;
        [SerializeField] private float _chaserChasingDistance;
        [SerializeField] private float _chaserAttackingDistance;

        [Header("Shooter")]
        [SerializeField] private float _shooterPatrollingSpeed;
        [SerializeField] private float _shooterAttackingForce;
        [SerializeField] private float _shooterAttackingDistance;
        [SerializeField] private float _shooterReloadingTime;

        #region CHASER_PARAMS
        public float chaserPatrollingSpeed { get { return _chaserPatrollingSpeed;  } }
        public float chaserChasingSpeed { get { return _chaserChasingSpeed;  } }
        public float chaserAttackingSpeed { get { return _chaserAttackingSpeed;  } }
        public float chaserChasingDistance { get { return _chaserChasingDistance;  } }
        public float chaserAttackingDistance { get { return _chaserAttackingDistance;  } }
        #endregion
        #region SHOOTER_PARAMS
        public float shooterPatrollingSpeed { get { return _shooterPatrollingSpeed; } }
        public float shooterAttackingForce { get { return _shooterAttackingForce; } }
        public float shooterAttackingDistance { get { return _shooterAttackingDistance; } }
        public float shooterReloadingTime { get { return _shooterReloadingTime; } }
        #endregion

        public static EnemyData instance { get; private set; }
        public event Action OnDataChanged;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        private void OnValidate()
        {
            OnDataChanged?.Invoke();
        }
    }
}
