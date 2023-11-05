using UnityEngine;
using System.Linq;

public class DamagingTrigger : MonoBehaviour
{
    private IDamager damager;

    private void Awake()
    {
        damager = GetComponentInParent<IDamager>();
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (damager.layersToDamage.Any(l => l == coll.gameObject.layer))
        {
            IDamagable obj = coll.GetComponent<IDamagable>();
            if (obj != null)
            {
                obj.GetDamage();
                damager.OnDamage();
            }
        }
    }
}
