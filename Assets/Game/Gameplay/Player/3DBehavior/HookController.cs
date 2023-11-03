using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{
    public class HookController : MonoBehaviour
    {
        [Header("Required components")]
        [Space]
        [SerializeField] internal Transform hook;
        //[SerializeField] internal Transform pivotHook;
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal Transform defaultPointHook;
        [SerializeField] internal Transform pointToRaiCast;

        [Header("The force with which the enemy will be thrown")]
        [Space]
        [SerializeField] internal float forceToThrowObject;

        [Header("Maximum range of the hook")]
        [Space]
        [Range(2, 10)] [SerializeField] internal float maxDistanseHook;
        //[Range(0, 9)] [SerializeField] internal float idleDistanseHook;
        [Header("Time intervals (it may be worth replacing with animation curve)")]
        [Header("throw, pullUp, time coyote, stun")]
        [Space]
        [Range(0f, 3f)] [SerializeField] internal float timeThrowHook;
        [Range(0f, 3f)] [SerializeField] internal float timePullUpHook;
        [Range(0, 1)] [SerializeField] private float timeCaiot;
        [Range(0, 5)] [SerializeField] internal float timeStun;
        [Header("Parameters of the assistant in the guidance")]
        [Space]
        [Range(0, 30)] [SerializeField] internal float triggerAngleAIM;
        [Range(0, 10)] [SerializeField] internal int countRaiAIM;
        [SerializeField] internal LayerMask layerToTouchAIM;
        [SerializeField] internal LayerMask layerWall;
        [SerializeField] internal LayerMask layerEnemyAndProjectile;
        [SerializeField] internal LayerMask layerGround;


        private Dictionary<Type, IHookBehavior> behavioraMap;
        internal IHookBehavior behaviorCurrent;
        internal IHookBehavior behaviorPrevios;

        [Header("Parameters for the convenience of debag")]
        [Space]
        [SerializeField] internal Vector3 direction;
        [SerializeField] public bool isActiveHook = false;
        [SerializeField] public bool isEndHook = true;
        [SerializeField] internal bool icCaptureSomthing = false;
        [SerializeField] internal float angleDirection;
        [SerializeField] internal GameObject capturedTarget;
        [SerializeField] internal IGrabable grabableTarget;
        [SerializeField] private PlayerData data;
        internal Vector3 startPos;
        internal Vector3 endPos;
        internal float current;
        internal float normalazedPercentOfMaxDistance; //coefficient for distance and time
        internal bool trigerToPullUp = false; // separates the throw and the attraction

        private void Start()
        {
            this.InitBehaviors();
            this.SetBehaviorDefault();
            data = FindObjectOfType<PlayerData>();
        }

        #region Events
        private void OnEnable()
        {
            EventSystem.OnDataPlayerChanged.AddListener(SetData);
        }

        private void OnDisable()
        {
            EventSystem.OnDataPlayerChanged.RemoveListener(SetData);
        }
        #endregion

        private void InitBehaviors()
        {
            this.behavioraMap = new Dictionary<Type, IHookBehavior>();
            this.behavioraMap[typeof(HookAIMBehavior)] = new HookAIMBehavior(this);
            this.behavioraMap[typeof(HookRotationBehavior)] = new HookRotationBehavior(this);
            this.behavioraMap[typeof(HookCatcEmptyhBehavior)] = new HookCatcEmptyhBehavior(this);
            this.behavioraMap[typeof(HookCatchPointBehavior)] = new HookCatchPointBehavior(this);
            this.behavioraMap[typeof(HookCathcEnemyAndProjectileBehavior)] = new HookCathcEnemyAndProjectileBehavior(this);
            this.behavioraMap[typeof(HookRotationWithObjectBehavior)] = new HookRotationWithObjectBehavior(this);
            this.behavioraMap[typeof(HookThrowCaptureObject)] = new HookThrowCaptureObject(this);
            this.behavioraMap[typeof(HookStunBehavior)] = new HookStunBehavior(this);
        }
        private void SetBehaviorDefault()
        {
            SetBehaviorRotation();
        }

        public void SetBehavior(IHookBehavior newBehavior)
        {
            if (this.behaviorCurrent != null)
                this.behaviorCurrent.Exit();

            if (this.behaviorCurrent != this.GetBehavior<HookStunBehavior>())
                this.behaviorPrevios = behaviorCurrent;

            this.behaviorCurrent = newBehavior;
            this.behaviorCurrent.Enter();
        }

        private IHookBehavior GetBehavior<T>() where T : IHookBehavior
        {
            var type = typeof(T);
            return this.behavioraMap[type];
        }

        private void FixedUpdate()
        {
            behaviorCurrent?.UpdateBehavior();
            CaiotTime();
        }

        private void CaiotTime()
        {
            if (isActiveHook)
            {
                Invoke("ResetIsActiveHook", timeCaiot);
            }
        }

        #region SetBehaviors
        public void SetBehaviorRotation()
        {
            var behavior = this.GetBehavior<HookRotationBehavior>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorStan()
        {
            var behavior = this.GetBehavior<HookStunBehavior>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorThrowCaptureObject()
        {
            var behavior = this.GetBehavior<HookThrowCaptureObject>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorRotationWithObject()
        {
            var behavior = this.GetBehavior<HookRotationWithObjectBehavior>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorCatchEnemyAndProjectile()
        {
            var behavior = this.GetBehavior<HookCathcEnemyAndProjectileBehavior>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorCarchPoint()
        {
            var behavior = this.GetBehavior<HookCatchPointBehavior>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorAIM()
        {
            var behavior = this.GetBehavior<HookAIMBehavior>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorCatchEmpty()
        {
            var behavior = this.GetBehavior<HookCatcEmptyhBehavior>();
            this.SetBehavior(behavior);
        }
        #endregion

        private void ResetIsActiveHook()
        {
            isActiveHook = false;
        }

        private void SetData()
        {
            this.forceToThrowObject = data.forceToThrowObject;
            this.maxDistanseHook = data.maxDistanseHook;
            this.timeThrowHook = data.timeThrowHook;
            this.timePullUpHook = data.timePullUpHook;
            this.timeCaiot = data.timeCaiot;
            this.timeStun = data.timeStun;
            this.triggerAngleAIM = data.triggerAngleAIM;
            this.countRaiAIM = data.countRaiAIM;
            this.layerToTouchAIM = data.layerToTouchAIM;
            this.layerWall = data.layerWall;
            this.layerEnemyAndProjectile = data.layerEnemyAndProjectile;
        }

        #region Gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, direction.normalized * maxDistanseHook);
            Gizmos.color = Color.green;

            angleDirection = AccessoryMetods.GetAngleFromVectorXZ(direction);

            Vector3 maxAngle = AccessoryMetods.GetVectorFromAngleXZ(angleDirection + triggerAngleAIM);
            Vector3 minAngle = AccessoryMetods.GetVectorFromAngleXZ(angleDirection - triggerAngleAIM);
            Gizmos.DrawRay(pointToRaiCast.position, maxAngle * maxDistanseHook);
            Gizmos.DrawRay(pointToRaiCast.position, minAngle * maxDistanseHook);
        }
        #endregion
    }
}