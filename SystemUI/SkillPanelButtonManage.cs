using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelButtonManage : UIButtonManage
{
    [Header("character in order")]
    public List<GameObject> characters = new List<GameObject>();
    public GameObject itemPrefab;
    public RectTransform contentRectTransform; 
    public int x;
    protected override void Start()
    {
        for (int i = 0; i < x; i++)
        {
            AddNewItem();
        }
    }
    void OnEnable()
    {
        
    }
    void AddNewItem()
    {
        // 实例化子项并添加到Content下
        GameObject newItem = Instantiate(itemPrefab, contentRectTransform);
        // 强制立即刷新布局
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
    }
}
