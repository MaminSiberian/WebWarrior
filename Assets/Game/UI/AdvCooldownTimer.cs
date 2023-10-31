using UnityEngine;
using YG;

public class AdvCooldownTimer : MonoBehaviour
{
    public bool addIsReady {  get; private set; }

    private float time = 60f;
    private static float timer = 0;
    private static AdvCooldownTimer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (addIsReady) return;

        if (timer >= time)
        {
            timer = 0;
            addIsReady = true;
        }
        else
            timer += Time.deltaTime;
        Debug.Log(timer);
    }
    public void StartTimer()
    {
        addIsReady = false;
    }
}
