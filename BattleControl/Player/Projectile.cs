using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public int damage = 30; // 子弹造成的伤害值
    private Rigidbody2D rigidbody1;
    public int life = 10000;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody1 = GetComponent<Rigidbody2D>();        
        // 计算从子弹位置到鼠标位置的方向
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mousePosition - (Vector2)transform.position).normalized;
        rigidbody1.velocity = directionToMouse * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") 
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // 减少敌人的生命值
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        life--;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}