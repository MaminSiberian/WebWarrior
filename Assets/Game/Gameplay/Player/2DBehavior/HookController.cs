using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookControl
{

    public class HookController : MonoBehaviour
    {
        [SerializeField] internal Camera mainCamera;
        [SerializeField] internal Transform hook;
        [SerializeField] internal Transform pivotHook;
        [Space]
        [SerializeField] internal float forceToThrowObject;
        [Space]
        [Range(2, 10)] [SerializeField] internal float maxDistanseHook;
        [Range(1, 9)] [SerializeField] internal float idleDistanseHook;
        [Space]
        [Range(0, 30)] [SerializeField] internal float triggerAngleAIM;
        [Range(0, 10)] [SerializeField] internal int countRaiAIM;
        [SerializeField] internal LayerMask layerToTouchAIM;
        [SerializeField] internal LayerMask layerWall;
        [SerializeField] internal LayerMask layerEnemyAndProjectile;
        [SerializeField] internal Transform pointToRaiCast;
        [Space]
        [Range(0f, 3f)] [SerializeField] internal float timeThrowHook;
        [Range(0f, 3f)] [SerializeField] internal float timePullUpHook;
        [Range(0, 1)] [SerializeField] private float timeCaiot;

        private Dictionary<Type, IHookBehavior> behavioraMap;
        internal IHookBehavior behaviorCurrent;
        [SerializeField] internal Vector2 direction;
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
        }
        private void SetBehaviorDefault()
        {
            SetBehaviorRotation();
        }

        public void SetBehavior(IHookBehavior newBehavior)
        {
            if (this.behaviorCurrent != null)
                this.behaviorCurrent.Exit();

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

        //public void SetBehaviorPullUpEmptyHook()
        //{
        //    var behavior = this.GetBehavior<HookPullUpEmptyBehavior>();
        //    this.SetBehavior(behavior);
        //}

        private void ResetIsActiveHook()
        {
            isActiveHook = false;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, direction * maxDistanseHook / 2);
            Gizmos.color = Color.green;

            angleDirection = AccessoryMetods.GetAngleFromVector(direction);
            //Debug.Log(transform.position + "  " +  direction + " "+ angleDirection);

            Vector2 maxAngle = AccessoryMetods.GetVectorFromAngle(angleDirection + triggerAngleAIM);
            Vector2 minAngle = AccessoryMetods.GetVectorFromAngle(angleDirection - triggerAngleAIM);
            Gizmos.DrawRay(pointToRaiCast.position, maxAngle * maxDistanseHook / 2);
            Gizmos.DrawRay(pointToRaiCast.position, minAngle * maxDistanseHook / 2);
        }
    }
}