using HookControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnit : MonoBehaviour
{
    [Header("Force to throw Object")]
    [Range(0f, 50f)] [SerializeField] private float forceToThrowObject;

    [Header("if you need to synchronize, then set the same offset")]
    [Range(0f, 1f)] [SerializeField] private float deltaTime;
    [Header("Loop or PingPong ")]
    [SerializeField] private bool isPigPong;
    [Header("Changes the initial direction  ")]
    [SerializeField] private bool inverseDirectrion;
    [Header("Behavior  ")]
    [SerializeField] private AnimationCurve moveCurve;
    [Header(" speed movement  ")]
    [Range(0f, 3f)] [SerializeField] private float speedMove;
    [Header("Patrol point TRANSFORM")]
    [SerializeField] private List<Transform> patrolPoints;
    [Header("Need to desynchronize with others")]

    private float current;
    private Vector3 startPos, endPos;
    private int iterator = 1;
    private int currentPatrolPos;
    [SerializeField] private Vector3 direction;
   // private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        current = deltaTime;

        if (patrolPoints.Count > 1)
        {
            startPos = transform.position;
            currentPatrolPos = inverseDirectrion ? patrolPoints.Count - 1 : 0;
            endPos = patrolPoints[currentPatrolPos].position;
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
        current += Time.fixedDeltaTime * speedMove;
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
        //else
        //{
        //    if (currentPatrolPos == patrolPoints.Count - 1)
        //        currentPatrolPos = 0;
        //    //SetPositions();
        //}


    }

    private void SetPositions()
    {
        current = 0;
        startPos = patrolPoints[currentPatrolPos].position;
        currentPatrolPos += iterator;
        endPos = patrolPoints[currentPatrolPos].position;
        direction = (endPos - startPos).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Throw " + other);
        if (other.GetComponent<HookController>() != null)
        {
            var player = other.GetComponent<HookController>();
            player.SetBehaviorStan();
            player.rb.AddForce(direction * forceToThrowObject, ForceMode.Impulse);
            //other.GetComponent<Rigidbody>().AddForce(direction * forceToThrowObject, ForceMode.Impulse);
        }
        if (other.CompareTag("Enemy"))
        {

        }
    }
}
