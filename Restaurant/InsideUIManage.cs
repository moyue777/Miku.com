using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class InsideUIManage : MonoBehaviour
{
    public Button returnButton;
    public GameObject Content;
    public List<Button> selectButton;
    public Button selectManage;
    public Canvas canvas;
    void Start()
    {
        
        if (returnButton != null)
        {
            returnButton.onClick.AddListener(() =>
            {
                SuperController.Instance.PauseStop();
                canvas.enabled = false;
            }
            );
        }
    }
}