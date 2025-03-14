using UnityEngine;

public class MyTrigger : MonoBehaviour
{
    protected bool touched;
    protected Rigidbody2D playerRigidbody;

    /// <summary>
    /// 玩家进入触发区域
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRigidbody = other.GetComponent<Rigidbody2D>();
            touched = true;
        }
    }

    /// <summary>
    /// 玩家退出触发区域
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            touched = false;
            playerRigidbody = null;
        }
    }

    /// <summary>
    /// 玩家按下F键
    /// </summary>
    protected virtual void OnFKeyPressed()
    {
    }
        
    public virtual void Update()
    {
        // 检测玩家是否按下了F键并且玩家静止
        if (Input.GetKeyDown(KeyCode.F) && touched && playerRigidbody != null && playerRigidbody.velocity.sqrMagnitude < 0.01f)
        {
            OnFKeyPressed();
        }
    }
}