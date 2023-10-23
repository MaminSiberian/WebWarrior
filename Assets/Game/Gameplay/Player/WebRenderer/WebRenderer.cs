using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float maxTime;
    //private Coroutine Web;
    // Start is called before the first frame update
    void Start()
    {
        lr.positionCount = 2;
        EventSystem.OnThrowHook.AddListener(SetPosition);
        
    }

    private void SetPosition()
    {
        StartCoroutine("Web");
    }

    private IEnumerator Web()
    {
        targetPoint.gameObject.SetActive(true);
        var currentTime = maxTime;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            lr.SetPosition(0, startPoint.position);
            lr.SetPosition(1, targetPoint.position);
             yield return null;

        }
        targetPoint.gameObject.SetActive(false);
        StopCoroutine("Web");

    }
}
