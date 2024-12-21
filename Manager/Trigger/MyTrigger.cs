using UnityEngine;
public class MyTrigger : MonoBehaviour
{
    private bool touched;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        // 检查进入触发器的是否是玩家
        if (other.CompareTag("Player"))
        {
            touched = true;
            Debug.Log("Player entered trigger area on " + gameObject.name);
            // 玩家进入触发区域，现在可以按F键加载场景
        }
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            touched = false;
            Debug.Log("Player left trigger area on " + gameObject.name);
            // 玩家离开触发区域，现在无法按F键加载场景
        }
    }
    protected virtual void OnFKeyPressed()
    {
    }
        
    void Update()
    {
        // 检测玩家是否按下了F键
        if (Input.GetKeyDown(KeyCode.F) && touched)
        {
            OnFKeyPressed();
        }
    }
}