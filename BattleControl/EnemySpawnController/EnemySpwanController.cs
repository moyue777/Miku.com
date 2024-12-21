using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public int enemyCount = 3; // 敌人数量计数器
    public int waves = 2;
    public GameObject  Enemy;
    void Start()
    {
        enemyCount = 0; // 初始时没有敌人
    }

    void Update()
    {
        if (waves > 0 && enemyCount == 0)
        {
            // 创建敌人
            GameObject enemy = Instantiate(Enemy);
            enemy.transform.position = transform.position; // 设置敌人的位置
            enemyCount++; // 增加敌人数量
            Debug.Log("敌人数量: " + enemyCount);
            waves --;
        }
    }
    // 当敌人死亡时调用此方法
    public void EnemyDied()
    {
        enemyCount--; // 减少敌人数量
        Debug.Log("敌人数量: " + enemyCount);
    }
}