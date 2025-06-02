using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 添加这一行

//2025.4.23 改为OnEnable主动修改sprite,待优化
public class SpriteManage : MonoBehaviour
{
    public bool isImage;
    public List<Sprite> sprites;
    public Sprite self_sprite;
    protected bool hasSprite = false;
    public virtual void Start()
    {
        if (!isImage)
        {
            if ( gameObject.GetComponent<SpriteRenderer>() != null)
            self_sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        else if (isImage)
        {
            self_sprite = gameObject.GetComponent<Image>().sprite;
        }

        if (self_sprite != null)
        {
            hasSprite = true;
        }
    }
    public virtual void OnEnable()
    {
        if (SuperController.Instance != null)
        { Condition condition = SuperController.Instance.GetCondition(); ChangeSprite(condition); }
    }
    public virtual void ChangeSprite(Condition condition)
    {
        if (hasSprite)
        {
            switch (condition)
            {
                case Condition.Day:
                    if (isImage)
                    {
                        gameObject.GetComponent<Image>().sprite = sprites[0];
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
                    }
                    break;
                case Condition.Night:
                    if (isImage)
                    {
                        gameObject.GetComponent<Image>().sprite = sprites[1]; // 假设sprites[1]是Night的Sprite
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                    }
                    break;
                case Condition.Emergency:
                    if (isImage)
                    {
                        gameObject.GetComponent<Image>().sprite = sprites[2]; // 假设sprites[2]是Emergency的Sprite
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
                    }
                    break;
            }
        }
    }
}