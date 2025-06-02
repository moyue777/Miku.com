using UnityEngine.UI;
using UnityEngine;

public partial class PlotManager
{
    public void LoadCharacter(string target_character)
    {
        Sprite target_sprite = Resources.Load<Sprite>("characters/" + target_character);
        if (target_sprite == null)
        {
            target_sprite = Resources.Load<Sprite>("characters/default");
        }
        current_character.sprite = target_sprite;
        current_character.SetNativeSize(); // 根据 Sprite 的原始尺寸调整大小

        current_character.rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }

    public void LoadVoice(string target_voice)
    {
        AudioClip target_audioClip = Resources.Load<AudioClip>("voices/" + target_voice);
        if (target_audioClip != null)
        {
            current_audioClip = target_audioClip;
        }
    }
}