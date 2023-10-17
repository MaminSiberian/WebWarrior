using NaughtyAttributes;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    [SerializeField] private bool isSelfDestructable;
    [SerializeField, ShowIf("isSelfDestructable")] private float lifetime;

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
