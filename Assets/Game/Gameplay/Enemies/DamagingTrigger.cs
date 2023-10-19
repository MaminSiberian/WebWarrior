using UnityEngine;

public class DamagingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<IDamagable>().GetDamage();
        }
    }
}
