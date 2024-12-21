using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public int savepos;
    public ButtonManage button_manage;
    void Start()
    {
        button_manage = GetComponentInParent<ButtonManage>();
    }
    public void LoadGame()
    {
        button_manage.LoadGame(savepos);
    }
}
