using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    // 定义两个位置进行限制相机移动
    public Vector2 minPosition;
    public Vector2 maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControl target_control= FindObjectOfType<PlayerControl>();
        target = target_control.transform;
    }

    public void SetTarget(Transform transform)
    {
        target = transform;
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            virtualCamera.Follow = transform;
        }
    }
}