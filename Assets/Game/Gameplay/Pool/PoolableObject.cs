using NaughtyAttributes;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    [SerializeField] private bool isSelfDestructable;
    [SerializeField, ShowIf("isSelfDestructable")] private float lifetime;

    private float timer = 0f;

    protected virtual void Update()
    {
        if (isSelfDestructable) TickTimer();
    }
    private void TickTimer()
    {
        if (timer >= lifetime)
        {
            timer -= lifetime;
            Deactivate();
        }
        else
            timer += Time.deltaTime;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
