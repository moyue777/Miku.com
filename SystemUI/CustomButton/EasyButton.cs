using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EasyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text text_skill;
    private Color origin_color;
    private bool isClicked = false;
    private Image buttonImage; 
    [Header("Sprites")]
    public Sprite sprite_default;
    public Sprite sprite_selected;
    public Sprite sprite_hover; // 新增悬停状态的Sprite

    void Start()
    {
        text_skill = GetComponentInChildren<Text>();
        origin_color = text_skill.color;
        buttonImage = GetComponent<Image>();
    }

    public void OnClick()
    {
        if (isClicked)
        {
            text_skill.color = origin_color;
            isClicked = false;
            buttonImage.sprite = sprite_default;
        }
        else
        {
            text_skill.color = Color.white;
            isClicked = true;
            buttonImage.sprite = sprite_selected;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            text_skill.color = Color.white;
            buttonImage.sprite = sprite_hover; // 设置为悬停状态的Sprite
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            text_skill.color = origin_color;
            buttonImage.sprite = sprite_default; // 恢复默认状态的Sprite
        }
    }
}