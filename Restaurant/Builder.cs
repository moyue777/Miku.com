using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Builder : MonoBehaviour
{
    public List<string> ingredients = new List<string>(); // 当前实体中的配料
    public Store store;
    private bool isDragging = false;
    private bool canDrag = true;
    private bool isWaiting = false;

    public int default_pos = -1;

    private Vector2 offset; // 鼠标点击位置与初始位置的偏移量
    public Vector2 defaultPosition; // 初始位置
    
    private TeaWaitingProcess processor;
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
            int waiting_time = 0;
            bool isplaced = false;
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Customer")) // 顾客的标签是"Customer"
                {
                    Debug.Log("find customer");
                    CustomControl customer = collider.GetComponentInParent<CustomControl>();
                    if (customer != null && customer.CheckStandby())
                    {
                        
                        isplaced = true;
                        customer.Receive(this);
                        Destroy(gameObject);
                        break;
                    }
                }
                if (collider.CompareTag("Cooker") && isWaiting == false) // 标签是"WaitingProcess"则进入5秒等待
                {
                    processor = collider.GetComponentInParent<TeaWaitingProcess>();
                    if (processor != null)
                    {
                        if(processor.findEmpty() == false)
                        {
                            continue;
                        }
                        isplaced = true;
                        AddIngredient(processor.Ingredient());
                        transform.position = processor.getEmptyPositon();
                        waiting_time = processor.getWaitTime();
                        isWaiting = true;
                        break;
                    }
                }
            }

            if (!isplaced && isWaiting == false)
            {
                transform.position = store.GetPosVector2(default_pos);
                
            }            
            if (isWaiting == true)
            {
                Waiting(waiting_time);
            }
            isDragging = false;
        }

        // 如果正在拖动，更新位置
        if (isDragging && canDrag)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition + offset;
        }
    }
    
    //外部加料接口
    public bool AddIngredient(string ingredient)
    {
        if (!ingredients.Contains(ingredient))
        {
            ingredients.Add(ingredient);
            Debug.Log("Added: " + ingredient);
            return true;
        }
        else{
            return false;
        }
    }
    

    //开始等待
    private void Waiting(int waiting_time)
    {
        canDrag = false;
        StartCoroutine(StartWaiting(waiting_time));
    }
    private IEnumerator StartWaiting(int waiting_time)
    {
        processor.BuilderStartWaiting();
        yield return new WaitForSeconds(waiting_time); // 等待指定时间
        processor.BuilderEndWaiting();
        canDrag = true; // 恢复拖动能力
        isWaiting = false;
    }
}