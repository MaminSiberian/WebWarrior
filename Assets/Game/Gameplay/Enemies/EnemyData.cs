using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyData : MonoBehaviour
    {
        [Header("Chaser")]
        [SerializeField] private float _chaserSpeed;

        public float chaserSpeed { get { return _chaserSpeed;  } }

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
