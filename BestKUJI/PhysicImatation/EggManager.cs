using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GachaSystem.Systems
{
    public class EggManager : MonoBehaviour
    {
        public GameObject eggPrefab;
        public BottomAddForce bottomAddForce;
        public List<Vector2> eggPositions;

        void Start()
        {
            bottomAddForce.isActive = true;
            foreach (var pos in eggPositions)
            {
                // 添加上下0.4的抖动
                float jitter = Random.Range(-0.4f, 0.4f);
                Vector2 newPosition = new Vector2(pos.x + jitter, pos.y + jitter);

                Instantiate(eggPrefab, newPosition, Quaternion.identity);
            }
        }

        public void Draw()
        {
            bottomAddForce.isActive = false;
        }
    }
}