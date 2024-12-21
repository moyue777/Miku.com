using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    // 定义两个位置进行限制相机移动
    public Vector2 minPosition;
    public Vector2 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPos = target.position;
            targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);

            // 保留当前的 z 坐标
            float currentZ = transform.position.z;

            // 只插值 x 和 y
            Vector3 newPosition = new Vector3(
                Mathf.Lerp(transform.position.x, targetPos.x, smoothing),
                Mathf.Lerp(transform.position.y, targetPos.y, smoothing),
                currentZ
            );

            transform.position = newPosition;
        }
    }
}