using System.Collections.Generic;
using UnityEngine;

public class ButtonManage : MonoBehaviour
{
    public List<GameObject> buttons = new List<GameObject>();
    public SuperController superController;
    void Start()
    {
        superController = SuperController.Instance;
    }

    public void UpdateButton()
    {
        
    }
    public void LoadGame(int savepos)
    {
        if (superController != null)
        {
            superController.LoadPlayerData(savepos);
            superController.LoadNewScene("HomeScene",2.0f);
        }
    }
}