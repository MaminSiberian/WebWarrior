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

        #region CHASER_PARAMS
        public float chaserPatrollingSpeed { get { return _chaserPatrollingSpeed;  } }
        public float chaserChasingSpeed { get { return _chaserChasingSpeed;  } }
        public float chaserAttackingSpeed { get { return _chaserAttackingSpeed;  } }
        public float chaserChasingDistance { get { return _chaserChasingDistance;  } }
        public float chaserAttackingDistance { get { return _chaserAttackingDistance;  } }
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
