using UnityEngine;
using UnityEngine.UI;
public class CharacterButton : MonoBehaviour
{
    private Text text_skill;
    private bool isClicked = false;
    private Image CharacterSprite; 
    [Header("Sprites")]
    public Sprite sprite_default_day;
    public Sprite sprite_default_night;
    public Sprite sprite_default_emergence;
    public Sprite sprite_selected_day;
    public Sprite sprite_selected_night;
    public Sprite sprite_selected_emergence;
    private Sprite sprite_default;
    private Sprite sprite_selected;
    void Start()
    {
        sprite_default = sprite_default_day;
        sprite_selected = sprite_selected_day;
        text_skill = GetComponentInChildren<Text>();
        CharacterSprite = GetComponent<Image>();
    }
    void OnEnable()
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

    public void OnClick()
    {
        if (isClicked)
        {
            isClicked = false;
            CharacterSprite.sprite = sprite_default;
        }
        else
        {
            isClicked = true;
            CharacterSprite.sprite = sprite_selected;
        }
    }
}