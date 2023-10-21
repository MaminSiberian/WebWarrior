using NaughtyAttributes;
using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyData : MonoBehaviour
    {
        #region CHASER_PARAMS
        [Header("Chaser")]
        [SerializeField] private bool showChaserParams;
        [SerializeField, ShowIf("showChaserParams")] private float _chaserPatrollingSpeed;
        [SerializeField, ShowIf("showChaserParams")] private float _chaserChasingSpeed;
        [SerializeField, ShowIf("showChaserParams")] private float _chaserAttackingSpeed;
        [SerializeField, ShowIf("showChaserParams")] private float _chaserChasingDistance;
        [SerializeField, ShowIf("showChaserParams")] private float _chaserAttackingDistance;
        public float chaserPatrollingSpeed { get { return _chaserPatrollingSpeed;  } }
        public float chaserChasingSpeed { get { return _chaserChasingSpeed;  } }
        public float chaserAttackingSpeed { get { return _chaserAttackingSpeed;  } }
        public float chaserChasingDistance { get { return _chaserChasingDistance;  } }
        public float chaserAttackingDistance { get { return _chaserAttackingDistance;  } }
        #endregion

        #region SHOOTER_PARAMS
        [Header("Shooter")]
        [SerializeField] private bool showShooterParams;
        [SerializeField, ShowIf("showShooterParams")] private float _shooterPatrollingSpeed;
        [SerializeField, ShowIf("showShooterParams")] private float _shooterAttackingForce;
        [SerializeField, ShowIf("showShooterParams")] private float _shooterAttackingDistance;
        [SerializeField, ShowIf("showShooterParams")] private float _shooterReloadingTime;
        public float shooterPatrollingSpeed { get { return _shooterPatrollingSpeed; } }
        public float shooterAttackingForce { get { return _shooterAttackingForce; } }
        public float shooterAttackingDistance { get { return _shooterAttackingDistance; } }
        public float shooterReloadingTime { get { return _shooterReloadingTime; } }
        #endregion

        #region CARRIER_PARAMS
        [Header("Carrier")]
        [SerializeField] private bool showCarrierParams;
        [SerializeField, ShowIf("showCarrierParams")] private float _carrierPatrollingSpeed;
        [SerializeField, ShowIf("showCarrierParams")] private float _carrierChasingSpeed;
        [SerializeField, ShowIf("showCarrierParams")] private float _carrierAttackingSpeed;
        [SerializeField, ShowIf("showCarrierParams")] private float _carrierChasingDistance;
        [SerializeField, ShowIf("showCarrierParams")] private float _carrierAttackingDistance;
        public float carrierPatrollingSpeed { get { return _carrierPatrollingSpeed; } }
        public float carrierChasingSpeed { get { return _carrierChasingSpeed; } }
        public float carrierAttackingSpeed { get { return _carrierAttackingSpeed; } }
        public float carrierChasingDistance { get { return _carrierChasingDistance; } }
        public float carrierAttackingDistance { get { return _carrierAttackingDistance; } }
        #endregion

        #region MINI_CHASER_PARAMS
        [Header("MiniChaser")]
        [SerializeField] private bool showMiniChaserParams;
        [SerializeField, ShowIf("showMiniChaserParams")] private float _miniChaserChasingSpeed;
        [SerializeField, ShowIf("showMiniChaserParams")] private float _miniChaserAttackingSpeed;
        [SerializeField, ShowIf("showMiniChaserParams")] private float _miniChaserChasingDistance;
        [SerializeField, ShowIf("showMiniChaserParams")] private float _miniChaserAttackingDistance;
        public float miniChaserChasingSpeed { get { return _miniChaserChasingSpeed; } }
        public float miniChaserAttackingSpeed { get { return _miniChaserAttackingSpeed; } }
        public float miniChaserChasingDistance { get { return _miniChaserChasingDistance; } }
        public float miniChaserAttackingDistance { get { return _miniChaserAttackingDistance; } }
        #endregion

        #region TURRET_PARAMS
        [Header("Turret")]
        [SerializeField] private bool showTurretParams;
        [SerializeField, ShowIf("showTurretParams")] private float _turretPatrollingSpeed;
        [SerializeField, ShowIf("showTurretParams")] private float _turretAttackingForce;
        [SerializeField, ShowIf("showTurretParams")] private float _turretAttackingDistance;
        [SerializeField, ShowIf("showTurretParams")] private float _turretReloadingTime;
        public float turretPatrollingSpeed { get { return _turretPatrollingSpeed; } }
        public float turretAttackingForce { get { return _turretAttackingForce; } }
        public float turretAttackingDistance { get { return _turretAttackingDistance; } }
        public float turretReloadingTime { get { return _turretReloadingTime; } }
        #endregion

        #region SPAWN_TURRET_PARAMS
        [Header("SpawnTurret")]
        [SerializeField] private bool showSpawnTurretParams;
        [SerializeField, ShowIf("showSpawnTurretParams")] private float _spawnTurretPatrollingSpeed;
        [SerializeField, ShowIf("showSpawnTurretParams")] private float _spawnTurretAttackingDistance;
        [SerializeField, ShowIf("showSpawnTurretParams")] private float _spawnTurretReloadingTime;
        public float spawnTurretPatrollingSpeed { get { return _spawnTurretPatrollingSpeed; } }
        public float spawnTurretAttackingDistance { get { return _spawnTurretAttackingDistance; } }
        public float spawnTurretReloadingTime { get { return _spawnTurretReloadingTime; } }
        #endregion

        public event Action OnDataChanged;

        private void OnValidate()
        {
            OnDataChanged?.Invoke();
        }
    }
}
