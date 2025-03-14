using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControl : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移动速度
    private Launcher launcher;
    private bool launched = false;
    private float moveHorizontal = 0.0f;

    // Update is called once per frame
    public void SetLaunch(bool haslaunched)
    {
        launched = haslaunched;
    }
    void Start()
    {
        launcher = GetComponentInChildren<Launcher>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            launched = true;
            launcher.Launch();
            return;
        }

        if (!launched)
        {
        // 检测左箭头键
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1.0f;
        }
        // 检测右箭头键
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1.0f;
        }

        // 计算移动向量
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        // 移动物体
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }
}