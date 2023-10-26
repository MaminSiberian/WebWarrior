using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if (UNITY_EDITOR) 
public class TiledObjectOnScaleSize : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabGFX;
    [SerializeField] private int count;
    [SerializeField] private float offset;

    [Button]
    private void Tiled()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => DestroyImmediate(child));

        if (transform.localScale.z >= 1)
        {
            count = (int)transform.localScale.z;
            float half = (float)count;
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
                 var rand = Random.Range(0, prefabGFX.Count);
                var obj = Instantiate(prefabGFX[rand], transform.position, transform.rotation);
                obj.transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    i + offset);
                obj.transform.SetParent(transform);
            }
        }
    }
}
#endif