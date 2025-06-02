using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// PlotManager 管理trigger和 TalkUIControl发送文本 
/// </summary>
public partial class PlotManager : MonoBehaviour
{
    public static PlotManager Instance; // 自己的单例
    [Header("performance setting")]
    public Image current_character;
    public AudioClip current_audioClip;
    public float scaleFactor = 0.5f;
    [Header("target canvas")]
    public GameObject talk_canva; // 对话canvas
    public Canvas back_canva; // backlog canvas
    public Text box_talk; // 对话文本框
    public GameObject talk_background;
    public Image perform_back;//演出背景
    [Header("performance file")]
    public AudioSource audioSource; // 添加音频源组件
    public PlotData current_plotdata; // 当前剧情
    private SuperController superController;

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
        talk_canva.SetActive(false);
        back_canva.enabled = false;
        current_string_num = 0;

        //Super control
        superController = SuperController.Instance;
    }

    /// <summary>
    /// 触发器调用,发送文本开始对话
    /// </summary>
    /// <param name="active_trigger_num"></param>
    public void TriggerSend(string active_trigger_num)
    {
        StartCoroutine(TriggerSendCoroutine(active_trigger_num));
    }

    private IEnumerator TriggerSendCoroutine(string active_trigger_num)
    {
        SuperController.Instance.PlayerStop();
        yield return null; // 等待一帧

        if (current_plotdata.updateList.active_triggers.Contains(active_trigger_num))
        {
            superController.ChangeTalking(true);
            SetDialogueText(active_trigger_num);
        }
    }
    /// <summary>
    /// TalkControl结束
    /// </summary>
    /// <param name="need_refresh"></param>
    public void EndTalk(bool need_refresh = true)
    {
        superController.ChangeTalking(false);
        if (need_refresh)
        {
            superController.FinishTalk();
        }
        talk_canva.SetActive(false);
    }
}