using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnit : MonoBehaviour
{
    [SerializeField] private bool isPigPong;
    [SerializeField] private AnimationCurve moveCurve;
    [Range(0f, 3f)] [SerializeField] private float speedMove;
    [SerializeField] protected List<Transform> patrolPoints;
    private float current;
    private Vector3 startPos, endPos;
    private int iterator = 1;
    private int currentPatrolPos;


    // Start is called before the first frame update
    void Start()
    {
        if (patrolPoints.Count > 1)
        {
            startPos = patrolPoints[currentPatrolPos].position;
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
        current += Time.deltaTime * speedMove;
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
        }
        else
        {
            if (currentPatrolPos == patrolPoints.Count - 1)
                currentPatrolPos = 0;
            else
                currentPatrolPos++;
        }

        current = 0;
        startPos = patrolPoints[currentPatrolPos].position;
        currentPatrolPos += iterator;
        endPos = patrolPoints[currentPatrolPos].position;
    }


}
