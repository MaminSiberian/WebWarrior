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
    }

    private void OnEnable()
    {
        EventSystem.OnThrowHook.AddListener(SetPosition);
        EventSystem.OnHookStan.AddListener(StopWebRenderer);
    }
    private void OnDisable()
    {
        EventSystem.OnThrowHook.RemoveListener(SetPosition);
        EventSystem.OnHookStan.RemoveListener(StopWebRenderer);
    }


    private void StopWebRenderer()
    {
        Debug.Log("stopWeb");
        lr.enabled = false;
        targetPoint.gameObject.SetActive(false);
        StopCoroutine(Web());
    }

    private void SetPosition()
    {
        StartCoroutine("Web");
    }

    private IEnumerator Web()
    {
        lr.enabled = true;
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
        lr.enabled = false;
        StopCoroutine("Web");

    }
}
