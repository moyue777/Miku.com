using UnityEngine;

public class CupDragManage : MonoBehaviour
{
    private bool isDragging = false;
    private bool canDrag = true;
    private Vector2 offset; // 鼠标点击位置与初始位置的偏移量
    public Vector2 defaultPosition; // 初始位置
    void Update()
    {
        // 处理左键按下，拖动
        if (Input.GetMouseButtonDown(0) && canDrag)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = (Vector2)transform.position - mousePosition; // 计算偏移量
            }
        }

        // 处理左键释放
        if (Input.GetMouseButtonUp(0) && isDragging && canDrag)
        {
            bool isColliding = false;
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Customer"))
                {

                    isColliding = true;
                }
            }
            if (!isColliding)
            {
                SetBack();
            }
        }

        // 如果正在拖动，更新位置
        if (isDragging && canDrag)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition + offset;
        }
    }

    /// <summary>
    /// 置回原位
    /// </summary>
    private void SetBack()
    {
        transform.position = defaultPosition;   
    }

}