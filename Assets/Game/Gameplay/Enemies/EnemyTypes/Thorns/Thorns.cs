using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private GameObject prefabGFX;
    [SerializeField] private int count;
    [SerializeField] private float offset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamagable>() != null)
        {
            Debug.Log(other + "ran into thorns");
            other.GetComponent<IDamagable>().GetDamage();
        }
    }

    [Button]
    private void Tiled()
    {
        if (transform.localScale.z >= 1)
        {
            count = (int)transform.localScale.z;
            float half = (float) count;
            if (half % 2 == 0)
            {
                offset = count / 2 - 0.5f;
            }
            else
            {
                offset = count / 2;
            }
            offset = transform.position.z - offset;
            for (int i = 0; i < count; i++)
            {
                var obj = Instantiate(prefabGFX, transform.position, transform.rotation);
                obj.transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    i + offset);
                obj.transform.SetParent(transform);
            }
        }
    }
}
