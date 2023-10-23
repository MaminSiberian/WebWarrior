using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace HookControl
{
    public class HookController : MonoBehaviour
    {
        [Header("Требуемые компоненты")]
        [Space]
        [SerializeField] internal Transform hook;
        //[SerializeField] internal Transform pivotHook;
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal Transform defaultPointHook;
        [SerializeField] internal Transform pointToRaiCast;

        [Header("Сила с которой будет кинут враг")]
        [Space]
        [SerializeField] internal float forceToThrowObject;

        [Header("Максимальная дальность зацепа и минимальное положение")]
        [Space]
        [Range(2, 10)] [SerializeField] internal float maxDistanseHook;
        [Range(0, 9)] [SerializeField] internal float idleDistanseHook;
        [Header("Временные интервалы (возможно стоит заменить на animation curve)")]
        [Header("бросок, притягивание, время кайота, стан")]
        [Space]
        [Range(0f, 3f)] [SerializeField] internal float timeThrowHook;
        [Range(0f, 3f)] [SerializeField] internal float timePullUpHook;
        [Range(0, 1)] [SerializeField] private float timeCaiot;
        [Range(0, 5)] [SerializeField] internal float timeStan;
        [Header("Параметры помошника в наведении")]
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

        [Header("Параметры для удобства дебага")]
        [Space]
        [SerializeField] internal Vector3 direction;
        [SerializeField] public bool isActiveHook = false;
        [SerializeField] public bool isEndHook = true;
        [SerializeField] internal bool icCaptureSomthing = false;
        [SerializeField] internal float angleDirection;
        [SerializeField] internal GameObject capturedTarget;
        // [SerializeField] internal float currentMaxDistanceHook;

        private void Start()
        {
            this.InitBehaviors();
            this.SetBehaviorDefault();
        }

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
            this.behavioraMap[typeof(HookStanBehavior)] = new HookStanBehavior(this);
        }
        private void SetBehaviorDefault()
        {
            SetBehaviorRotation();
        }

        public void SetBehavior(IHookBehavior newBehavior)
        {
            if (this.behaviorCurrent != null)
                this.behaviorCurrent.Exit();

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

        public void SetBehaviorRotation()
        {
            var behavior = this.GetBehavior<HookRotationBehavior>();
            this.SetBehavior(behavior);
        }

        public void SetBehaviorStan()
        {
            var behavior = this.GetBehavior<HookStanBehavior>();
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

        private void ResetIsActiveHook()
        {
            isActiveHook = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, direction * maxDistanseHook / 2);
            Gizmos.color = Color.green;

            angleDirection = AccessoryMetods.GetAngleFromVectorXZ(direction);

            Vector3 maxAngle = AccessoryMetods.GetVectorFromAngleXZ(angleDirection + triggerAngleAIM);
            Vector3 minAngle = AccessoryMetods.GetVectorFromAngleXZ(angleDirection - triggerAngleAIM);
            Gizmos.DrawRay(pointToRaiCast.position, maxAngle * maxDistanseHook / 2);
            Gizmos.DrawRay(pointToRaiCast.position, minAngle * maxDistanseHook / 2);
        }

        [Button]
        private void TestStan()
        {
            SetBehaviorStan();
        }
    }

}