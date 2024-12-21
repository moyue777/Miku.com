using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePosition; // 发射位置
    public GameObject projectile; // 投掷物预制体
    public float attackCooldown = 1f; // 攻击冷却时间，单位为秒
    public float fireRate = 0.5f; // 发射频率，单位为秒


    private float attackTimer = 0f; // 攻击计时器

    void Update()
    {
        // 减少计时器，无论是否在冷却时间内
        attackTimer -= Time.deltaTime;

        // 检测鼠标左键是否被按住
        if (Input.GetMouseButton(0)) // 0 代表鼠标左键
        {
            // 检查是否已经过了足够的冷却时间
            if (attackTimer <= 0f)
            {
                // 实例化投掷物
                Instantiate(projectile, firePosition.position, firePosition.rotation);

                // 重置攻击计时器
                attackTimer = fireRate;
            }
        }
        else
        {
            // 如果鼠标左键没有被按住，重置攻击计时器
            attackTimer = 0f;
        }
    }
}