using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnter : MonoBehaviour
{
    public ButtonManage buttonManage;  
    public void Onclick()
    {
        buttonManage.LoadGame(0);
    }
}
