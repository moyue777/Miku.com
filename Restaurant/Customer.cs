using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro; // 引入 TextMeshPro 命名空间

public class Customer : MonoBehaviour
{
    public Dictionary<string, bool> required_product = new Dictionary<string, bool>();//当前点单
    public List<string> ingredients = new List<string>();
    public string target_product;
    public int price;
    public int pos;//店内的位置

    public Order order;
    public Vector2 door; // 出发位置
    public Vector3 targetScale; // 目标缩放大小
    private float scaleTime = 1.0f; //缩放时间

    public GameObject orderBar;//点单小票
    private GameObject instantiatedOrderBar;

    private Vector2 selfscale;
    private float move_time = 0f;
    private bool standby = false;
    private float money = 0;
    private Vector2 origin_scale;

    void Start()
    {
        target_product = required_product.ElementAt(0).Key;
        origin_scale = transform.localScale;
    }

    // 调用这个函数来开始移动物体
    public void SetPos(Vector2 targetPos)
    {
        selfscale = transform.localScale;
        StartCoroutine(MoveObjectToPosition(door, targetPos, move_time + scaleTime, 8, false, true));
    }

    // 接受
    public void Receive(Builder current_cup)
    {
        HealthControl health = GetComponentInParent<HealthControl>();

        CalculateSatisfy(current_cup);

        if (order != null)
        {
            Debug.Log("price" + price);
            order.hasServed(pos, current_cup.default_pos, money * price);
            ShowMoneyText(money * price); // 新增调用
        }
        else
        {
            Debug.LogError("Order script not found on any object.");
        }

        standby = false;
        health.Die();
        Quit(true);
    }

    private void CalculateSatisfy(Builder current_cup)
    {
        float cur_satisfy = 0;
        List<string> target_ingredients = order.GetIngredients(required_product.ElementAt(0).Key);
        foreach (var item in current_cup.ingredients)
        {
            if (target_ingredients.Contains(item))
            {
                cur_satisfy += 1f;
            }
        }
        money += cur_satisfy / target_ingredients.Count;
    }

    // 移动顾客
    public void Quit(bool usual_quit = false)
    {
        if (!usual_quit)
        {
            order.hasServed(pos);
        }

        Destroy(instantiatedOrderBar);
        StartCoroutine(MoveObjectToPosition(transform.position, door, move_time, 0, true, false));
        StartCoroutine(DestroyAfterMove());
    }

    IEnumerator MoveObjectToPosition(Vector2 startPosition, Vector2 targetPosition, float duration, float threshold, bool firstScale = false, bool needx = false)
    {
        float time = 0;
        bool init = false;
        if (firstScale)
        {
            time = 0;
            while (time < scaleTime)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, origin_scale, time / scaleTime);
                time += Time.deltaTime;
                yield return null;
            }
        }

        time = 0;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;

            // 检查 x 坐标是否小于阈值
            if (transform.position.x < threshold && needx && !init)
            {
                InitialOrder();
                init = true;
            }

            yield return null;
        }
        transform.position = targetPosition; // 确保物体正好在目标位置

        if (!firstScale)
        {
            time = 0;
            while (time < scaleTime)
            {
                transform.localScale = Vector2.Lerp(selfscale, targetScale, time / scaleTime);
                time += Time.deltaTime;
                yield return null;
            }
        }
        transform.localScale = targetScale; // 放大物体
        Debug.Log("health generate");
        InitialCallBar();
        standby = true;
    }

    IEnumerator DestroyAfterMove()
    {
        yield return new WaitForSeconds(move_time);
        Destroy(gameObject);
    }

    public void SetMoveTime(float time)
    {
        move_time = time;
    }

    // OrderBar获取订单
    public string GetProduct()
    {
        return target_product;
    }

    // 外部获取standby
    public bool CheckStandby()
    {
        return standby;
    }

    private void InitialCallBar()
    {
        HealthControl health = gameObject.GetComponent<HealthControl>();
        health.beginWaiting();
    }

    private void InitialOrder()
    {
        // 实例化OrderBar
        instantiatedOrderBar = Instantiate(orderBar);
        instantiatedOrderBar.transform.SetParent(transform, false);

        OrderBar bar = instantiatedOrderBar.GetComponent<OrderBar>();
        bar.custom = this;

        Canvas canva = GetComponentInChildren<Canvas>();
        canva.enabled = true;
    }

    // 新增方法：显示金额文本
    private void ShowMoneyText(float amount)
    {
        Debug.Log("Entered show");
        GameObject textObject = new GameObject("MoneyText");
        textObject.transform.position = transform.position + new Vector3(1.6f, 0.5f, 0);

        Debug.Log("Added TextMeshPro");
        TextMeshPro textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = "+" + amount.ToString("F1"); // 显示两位小数
        textMesh.fontSize = 9; // 设置字体大小
        textMesh.color = Color.black;
        textMesh.alignment = TextAlignmentOptions.Center;

        // 设置文本对象的生命周期
        StartCoroutine(DestroyTextObject(textObject, 2.0f)); // 2秒后销毁
    }
    // 销毁文本对象的协程
    private IEnumerator DestroyTextObject(GameObject textObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(textObject);
    }
}