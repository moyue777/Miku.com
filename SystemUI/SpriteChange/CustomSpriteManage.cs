using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomImageManage : SpriteManage
{
    public Image cur_image;
    public override void Start()
    {
        self_sprite = cur_image.sprite;
    }
}
