using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

/// <summary>
/// PlotManager 管理trigger和 TalkUIControl发送文本 
/// </summary>
public partial class PlotManager : MonoBehaviour
{
    public static PlotManager Instance; // 自己的单例
    public SuperController superController;
    // 单例实现
    private void Awake()
    {
        // 单例实现
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        //TalkUIControl
        talk_canva.enabled = false;
        back_canva.enabled = false;
        current_string_num = 0;

        //Super control
        superController = FindObjectOfType<SuperController>();
    }

    /// <summary>
    /// 触发器调用,发送文本开始对话
    /// </summary>
    /// <param name="active_trigger_num"></param>
    public void TriggerSend(string active_trigger_num)
    {
        if (current_plotdata.updateList.active_triggers.Contains(active_trigger_num))
        {
            SetDialogueText(active_trigger_num);
            superController.ChangeTalking(true);
        }
    }

    /// <summary>
    /// 按钮调用,talkuicontrol结束
    /// </summary>
    /// <param name="need_refresh"></param>
    public void EndTalk(bool need_refresh = true)
    {
        superController.ChangeTalking(false);
        if (need_refresh)
        {
            superController.FinishTalk();
        }
        talk_canva.enabled = false;
    }
}