using System.Collections.Generic;
using UnityEngine;

public class ButtonManage : MonoBehaviour
{
    public List<GameObject> buttons = new List<GameObject>();
    public SuperController superController;
    public GameObject button_prefab;
    void Start()
    {
        superController = SuperController.Instance;
    }

    public void UpdateButton()
    {

    }

    /// <summary>
    /// 加载游戏，仅供测试
    /// </summary>
    /// <param name="savepos"></param>
    public void LoadGame(int savepos)
    {
        if (superController != null)
        {
            superController.LoadPlayerData(savepos);
            superController.LoadNewScene("HomeScene", 2.0f);
        }
    }
}