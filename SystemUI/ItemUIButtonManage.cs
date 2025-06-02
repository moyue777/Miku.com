using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIButtonManage : UIButtonManage
{
    public RectTransform contentRectTransform;
    public Button scrollButton;
    public int x = 5;
    private int currentItemIndex = 0; // 当前显示的项目索引
    private ScrollRect scrollRect;
    private List<string> rules;
    protected override void Start()
    {
        scrollRect = contentRectTransform.GetComponent<ScrollRect>();
        scrollRect.enabled = false; // 禁用ScrollRect以防止默认滚动
        scrollButton.onClick.AddListener(ScrollToNextItem); // 添加按钮点击事件
    }

    void ScrollToNextItem()
    {
        if (currentItemIndex < rules.Count - 1)
        {
            currentItemIndex++;
            float itemHeight = 1f;//contentRectTransform.GetChild(currentItemIndex)..height;
            float contentHeight = contentRectTransform.rect.height;
            float normalizedPositionY = 1f - ((currentItemIndex * itemHeight) / (contentHeight - itemHeight));
            scrollRect.normalizedPosition = new Vector2(0, normalizedPositionY);
        }
    }
}