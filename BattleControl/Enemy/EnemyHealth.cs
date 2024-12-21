using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100; // 敌人的生命值
    public int deathThreshold = 0; // 死亡阈值
    public EnemySpawnerController spawnerController; // 敌人生成控制器的引用

    private void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞对象是否是子弹
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Projectile bullet = collision.gameObject.GetComponent<Projectile>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage); // 减少生命值
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // 减少生命值
        Debug.Log("敌人受到 " + damage + " 点伤害，剩余生命值: " + health);

        if (health <= deathThreshold)
        {
            Die(); // 当生命值小于等于死亡阈值时死亡
        }
    }

    private void Die()
    {
        if (spawnerController != null)
        {
            spawnerController.EnemyDied(); // 通知敌人生成控制器敌人已死亡
        }

        // 销毁敌人对象
        Destroy(gameObject);
    }
}