using UnityEngine;
using NaughtyAttributes;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private int poolCount = 3;
    [SerializeField] private Projectile prefab;

    private static Pool<Projectile> pool;

    private void Start()
    {
        pool = new Pool<Projectile>(this.prefab, this.poolCount, this.transform);
    }

    [Button]
    public static Projectile GetProjectile()
    {
        return pool.GetObject();
    }
}
