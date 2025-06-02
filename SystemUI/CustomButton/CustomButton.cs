using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using SystemUI;
using Unity.VisualScripting;

public class CustomButton : Button
{
    [Header("Sprites")]
    public Sprite sprite_default_day;
    public Sprite sprite_default_night;
    public Sprite sprite_default_emergence;
    public Sprite sprite_selected_day;
    public Sprite sprite_selected_night;
    public Sprite sprite_selected_emergence;
    
    private Sprite sprite_default;
    private Sprite sprite_selected;


    private int button_num = -1;
    private SystemUIButtonManage systemUIButtonManage;
    public void SetSystemUIButtonManage(SystemUIButtonManage target_systemUIButtonManage)
    {
        systemUIButtonManage = target_systemUIButtonManage;
    }
    public void Setbutton_num(int target_num)
    {
        button_num = target_num;
    }
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        Condition condition = FindObjectOfType<SpriteChanger>().GetCondition();
        switch (condition)
        {
            case Condition.Day:
                sprite_default = sprite_default_day;
                sprite_selected =sprite_selected_day;
            break;
            case Condition.Night:
                sprite_default = sprite_default_night;
                sprite_selected =sprite_selected_night;
            break;
            case Condition.Emergency:
                sprite_default = sprite_default_emergence;
                sprite_selected =sprite_selected_emergence;
            break;
        }
    }

    // 当按钮进入高亮状态时调用
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        SetText(true);
    }

    // 当按钮退出高亮状态时调用
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        SetText();
    }

    private void SetText(bool needLoad = false)
    {
        if (systemUIButtonManage != null)
        {
            if (needLoad)
            { systemUIButtonManage.LoadText(button_num); }
            else
            { systemUIButtonManage.LoadText(-1); }
        }

    }
}