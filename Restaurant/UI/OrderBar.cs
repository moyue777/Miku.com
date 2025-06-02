using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderBar : MonoBehaviour
{
    public List<TMP_Text> allTextMeshPros;
    public CustomControl custom;
    private List<string> order;

    // Update is called once per frame
    void Start()
    {
        custom = GetComponentInParent<CustomControl>();
        allTextMeshPros = new List<TMP_Text>(GetComponentsInChildren<TMP_Text>());

        if (custom == null)
        {
            Debug.LogError("Custom is null. Ensure that a Customer component is present in the parent hierarchy.");
        }

        if (allTextMeshPros.Count == 0)
        {
            Debug.LogError("No TMP_Text components found in children. Ensure that there are TMP_Text components in the hierarchy.");
        }

        // 初始化订单
        initailOrder();
    }

    public void initailOrder()
    {
        if (custom == null)
        {
            Debug.LogError("Custom is null. Cannot initialize order.");
            return;
        }

        if (allTextMeshPros.Count == 0)
        {
            Debug.LogError("No TMP_Text components found in children. Cannot initialize order.");
            return;
        }

        // 获取产品名称并显示
        string product = custom.GetProduct();
        if (string.IsNullOrEmpty(product))
        {
            Debug.LogWarning("Product name is empty or null.");
        }
        else
        {
            allTextMeshPros[0].text = product;
            for (int i = 0; i < custom.ingredients.Count; i++)
            {
                allTextMeshPros[i + 1].text = custom.ingredients[i];
            }
        }

        // 如果需要显示多个订单项，取消注释以下代码
        /*
        if (order == null || order.Count == 0)
        {
            Debug.LogWarning("Order list is empty or null.");
            return;
        }

        for (int i = 0; i < order.Count; i++)
        {
            if (i < allTextMeshPros.Count && allTextMeshPros[i] != null)
            {
                allTextMeshPros[i].text = order[i];
                Debug.Log("Current text: " + order[i]);
            }
            else
            {
                Debug.LogWarning($"No TMP_Text component found for order item at index {i}.");
            }
        }
        */
    }
}