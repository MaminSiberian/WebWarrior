using HookControl;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnit : MonoBehaviour
{
    [Header("Patrol point TRANSFORM")]
    [SerializeField] private List<Transform> patrolPoints;
    [Header("Force to throw Object")]
    [Range(0f, 100f)][SerializeField] private float forceToPushObject;
    [SerializeField] private float timeStunEnemy;
    [SerializeField] private AnimationCurve forceCurve;
    [Header("Duration of force action")]
    [SerializeField] private float duration;
    [SerializeField] bool StopMovingAfterFinished = true;
    [Header("Need to desynchronize with others")]
    [Header("if you need to synchronize, then set the same offset")]
    [Range(0f, 1f)][SerializeField] private float deltaTime;
    [Header("Loop or PingPong ")]
    [SerializeField] private bool isPigPong;
    [Header("Changes the initial direction  ")]
    [SerializeField, ShowIf("isPigPong")] private bool inverseDirectrion;
    [Header("Behavior  ")]
    [SerializeField] private AnimationCurve moveCurve;
    [Header(" speed movement  ")]
    [Range(0f, 3f)][SerializeField] private float speedMove;
    [SerializeField] private float referenceLength;

    private float current;
    private Vector3 startPos, endPos;
    private int iterator = 1;
    private int currentPatrolPos;
    private float length;
    private float coefficient;
    private Vector3 direction;

    void Start()
    {
        if (!isPigPong)
        {
            inverseDirectrion = false;
        }
        current = deltaTime;
        if (patrolPoints.Count > 1)
        {
            startPos = transform.position;
            currentPatrolPos = inverseDirectrion ? patrolPoints.Count - 1 : 0;
            endPos = patrolPoints[currentPatrolPos].position;
            GetDirection();
            GetCurrentSpeed();
        }
    }

    private void FixedUpdate()
    {
        if (patrolPoints.Count == 0)
            return;

        CheckPatrollingPoints();
        MoveEnemy();
    }

    protected virtual void MoveEnemy()
    {
        current += Time.fixedDeltaTime * coefficient * speedMove;
        transform.position = Vector3.Lerp(startPos, endPos, moveCurve.Evaluate(current));
    }

    protected void CheckPatrollingPoints()
    {
        if (current > 1)
            SwitchPatrollingPoint();
    }
    protected void SwitchPatrollingPoint()
    {
        if (isPigPong)
        {
            if (currentPatrolPos == patrolPoints.Count - 1)
                iterator = -1;
            if (currentPatrolPos <= 0)
                iterator = 1;
            SetPositions();
        }
        else
        {
            current = 0;
            startPos = patrolPoints[currentPatrolPos].position;
            if (currentPatrolPos == patrolPoints.Count - 1)
                currentPatrolPos = 0;
            else
                currentPatrolPos += iterator;
            endPos = patrolPoints[currentPatrolPos].position;
            GetDirection();
            GetCurrentSpeed();
        }

    }

    private void SetPositions()
    {
        current = 0;
        startPos = patrolPoints[currentPatrolPos].position;
        currentPatrolPos += iterator;
        endPos = patrolPoints[currentPatrolPos].position;

        GetDirection();
        GetCurrentSpeed();
    }

    private void GetCurrentSpeed()
    {
        length = Vector3.Distance(startPos, endPos);
        coefficient = referenceLength / length;
    }

    private void GetDirection()
    {
        direction = (endPos - startPos).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Throw " + other);
        if (other.GetComponent<HookController>() != null)
        {
            PushPlayer(other);
            return;
        }
        if (other.GetComponent<IGrabable>() != null)
        {
            var obj = other.GetComponent<IGrabable>();
            if (other.GetComponent<Rigidbody>() != null)
            {
                var rb = other.GetComponent<Rigidbody>();
                obj.OnGrab();
                StartCoroutine (ReleaseObject(obj));
                StartCoroutine (PushObject(direction, rb));
                //rb.AddForce(direction * forceToPushObject, ForceMode.Impulse);

                //PushObject(direction, rb);

            }
            else
                Debug.Log("Objech Has no rigidbody");
        }
    }

    private IEnumerator ReleaseObject(IGrabable _object)
    {
        yield return new WaitForSeconds(timeStunEnemy);
        _object.OnRelease();
        StopCoroutine(ReleaseObject(_object));
    }

    private void PushPlayer(Collider other)
    {
        var player = other.GetComponent<HookController>();
        player.SetBehaviorStan();
        StartCoroutine(PushObject(direction, player.rb));
    }

    private IEnumerator PushObject(Vector3 direction, Rigidbody rb)
    {
        var startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            var t = (Time.time - startTime) / duration;
            rb.velocity = direction * forceToPushObject * forceCurve.Evaluate(t);
            yield return new WaitForFixedUpdate();
        }

        if (StopMovingAfterFinished)
            rb.angularVelocity = rb.velocity = Vector3.zero;

        StopCoroutine(PushObject(direction, rb));
    }
}
