using NaughtyAttributes;
using UnityEngine;

namespace Enemies
{
    public class MiniChaserPool : MonoBehaviour
    {
        [SerializeField] private int poolCount = 3;
        [SerializeField] private MiniChaser prefab;

        private static Pool<MiniChaser> pool;

        private void Start()
        {
            pool = new Pool<MiniChaser>(this.prefab, this.poolCount, this.transform);
        }

        [Button]
        public static MiniChaser GetMiniChaser()
        {
            return pool.GetObject();
        }
    }

}
