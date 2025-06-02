using UnityEngine;
using UnityEngine.UI;

public class TextShowChanger : MonoBehaviour
{
    private Text textComponent; // 用于存储 Text 组件

    public virtual void Start()
    {
        // 获取 Text 组件
        textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.LogError("Text component not found on this GameObject.");
        }
    }

    public virtual void OnEnable()
    {
        if (SuperController.Instance != null)
        {
            Condition condition = SuperController.Instance.GetCondition();
            ChangeSprite(condition);
        }
    }

    public virtual void ChangeSprite(Condition condition)
    {
        if (textComponent == null)
        {
            Debug.LogError("Text component not found on this GameObject.");
            return;
        }

        switch (condition)
        {
            case Condition.Day:
                textComponent.color = new Color(1f, 0.6f, 0f);
                break;
            case Condition.Night:
                textComponent.color = new Color(0f, 0.56f, 1f); // 设置夜晚的颜色
                break;
            case Condition.Emergency:
                textComponent.color = new Color(1f, 0f, 0f); // 设置紧急情况的颜色
                break;
            default:
                textComponent.color = new Color(1f, 0.6f, 0f); // 设置默认颜色
                break;
        }
    }
}