using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteManage : SpriteManage
{
    public override void Start()
    {
        self_sprite = gameObject.GetComponent<Button>().spriteState.highlightedSprite;
        if (self_sprite != null)
        {
            hasSprite = true;
        }
    }
    public override void ChangeSprite(Condition condition)
    {
        if (hasSprite)
        {
            Button button = gameObject.GetComponent<Button>();
            SpriteState spriteState = button.spriteState;

            switch (condition)
            {
                case Condition.Day:
                    spriteState.highlightedSprite = sprites[0];
                    break;
                case Condition.Night:
                    spriteState.highlightedSprite = sprites[1];
                    break;
                case Condition.Emergency:
                    spriteState.highlightedSprite = sprites[2];
                    break;
            }

            button.spriteState = spriteState;
        }
    }
}