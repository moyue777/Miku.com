using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 处理对话过程的工具
/// </summary>
public partial class PlotManager : MonoBehaviour
{
    //对话用
    private string active_trigger_num;
    private Dialogue out_dialogue; // 当前应该输出的对话
    private int current_string_num; // 当前应该输出的对话的index
    private Coroutine textUpdateCoroutine; // 加载文本的协程
    private List<Dialogue> current_dialogues;


    string mid;
    string target;
    private bool isBlackBackground = false;
    private bool isMid = false;

    public RectTransform center;
    public RectTransform origin;
    //选择用
    [Header("choice Manage")]
    public ChoiceManage choiceManage;
    private bool playerchose;
    private int choiceNum;
    private List<(string choice, string answer)> options = new List<(string, string)>();
    public void RefreshScene()
    {
        talk_canva.SetActive(false);
        DialogueEnd(false);
    }

    /// <summary>
    /// 外部调用，开始对话
    /// </summary>
    /// <param name="current_trigger_num">能够推动剧情的trigger</param>
    public void SetDialogueText(string current_trigger_num)
    {
        isBlackBackground = false;
        current_string_num = 0;
        talk_canva.SetActive(true);
        active_trigger_num = current_trigger_num;

        foreach (var scripts in current_plotdata.scripts)
        {
            if (scripts.names == active_trigger_num)
            {
                current_dialogues = scripts.dialogues;
                break;
            }
        }

        ClickNext();
    }

    /// <summary>
    /// 对话页面调用，跳过
    /// </summary>
    public void jump()
    {
        SuperController.Instance.PlayerRestore();
        DialogueEnd(active_trigger_num == current_plotdata.waiting_trigger);
    }

    /// <summary>
    /// 对话页面调用，处理点击
    /// </summary>
    public void ClickNext()
    {
        if (textUpdateCoroutine != null)
        {
            if (playerchose)
            {
                StopCoroutine(textUpdateCoroutine);
                textUpdateCoroutine = null;
                box_talk.text = mid + target; // 清空文本
            }
        }
        else
        {
            textUpdateCoroutine = StartCoroutine(UpdateTextCoroutine());
        }
    }

    /// <summary>
    /// 对话页面调用，获取backlog
    /// </summary>
    /// <returns></returns>
    public List<Dialogue> GetBacklogs()
    {
        List<Dialogue> backlogs = new List<Dialogue>();
        for (int i = 0; i < current_string_num; i++)
        {
            backlogs.Add(current_dialogues[i]);
        }
        return backlogs;
    }

    /// <summary>
    /// 获得当前应该输出的文本
    /// </summary>
    /// <returns></returns>
    private Dialogue GetCurrentDialogue()
    {
        if (active_trigger_num == "n")
        {
            return null;
        }

        if (current_string_num >= current_dialogues.Count)
        {
            Debug.Log("Dialogue end");
            DialogueEnd(active_trigger_num == current_plotdata.waiting_trigger);
            return null;
        }
        return current_dialogues[current_string_num++];
    }

    /// <summary>
    /// 用于显示文本，播放音频的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateTextCoroutine()
    {
        playerchose = false;
        SuperController.Instance.PauseStart();

        out_dialogue = GetCurrentDialogue();
        if (out_dialogue == null)
        {
            yield break;
        }

        target = out_dialogue.text;
        mid = "";
        switch (out_dialogue.character)
        {
            case "旁白":
                SetMid(false);
                SetBackGround(false);
                break;
            case "work":
                SuperController.Instance.Set_needWork();
                SetMid(false);
                SetBackGround(true);
                break;
            case "sleep":
                SuperController.Instance.Set_needSleep();
                SetBackGround(true);
                break;
            case "选择":
                HandleChoiceDialogue();
                yield return new WaitUntil(() => playerchose);
                Debug.Log("choiceNum: " + choiceNum);
                target = options[choiceNum].answer;
                break;
            case "连续":
                mid = box_talk.text;
                SetMid(true);
                SetBackGround(false);
                break;
            default:
                SetMid(false);
                SetBackGround(true);
                break;
        }
        playerchose = true;
        SuperController.Instance.PauseStop();
        box_talk.text = "";
        LoadCharacter(out_dialogue.character);
        LoadVoice(out_dialogue.voice);
        // 播放音频
        if (audioSource != null && current_audioClip != null)
        {
            audioSource.clip = current_audioClip;
            audioSource.Play();
        }
        box_talk.text = mid;
        foreach (char c in target.ToCharArray())
        {
            box_talk.text += c;
            yield return new WaitForSecondsRealtime(0.1f); // 调整延迟时间以控制显示速度
        }
        textUpdateCoroutine = null; // 协程结束后将变量设为null
    }

    /// <summary>
    /// 解析选择字符串
    /// </summary>
    private void HandleChoiceDialogue()
    {
        // 解析选项和答案
        options.Clear();
        string[] choices = out_dialogue.text.Split(';');

        foreach (var choice in choices)
        {
            string[] parts = choice.Split('|');
            if (parts.Length == 2)
            {
                string optionText = parts[0].Trim();
                string answerText = parts[1].Trim();
                options.Add((optionText, answerText));
            }
        }

        Debug.Log("options: " + options.Count);
        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            GameObject eventSystemObject = new GameObject("EventSystem");
            eventSystem = eventSystemObject.AddComponent<EventSystem>();

            // 添加 StandaloneInputModule
            eventSystemObject.AddComponent<StandaloneInputModule>();
        }
        choiceManage.InitialChoice(options.Count, options.Select(x => x.Item1).ToList(), true);
    }

    /// <summary>
    /// 选择UI调用，获得玩家选择
    /// </summary>
    /// <param name="choice"></param>
    public void OnOptionClick(int num)
    {
        playerchose = true;
        choiceNum = num;
    }

    /// <summary>
    /// 设置背景透明度
    /// </summary>
    /// <param name="istransparent"></param>
    private void SetBackGround(bool istransparent)
    {
        if ((isBlackBackground && !istransparent) || (!isBlackBackground && istransparent))
        {
            return;
        }
        Color currentColor = perform_back.color;
        if (istransparent)
        { currentColor.a = 0.5f; }
        else
        { currentColor.a = 1f; }
        isBlackBackground = !istransparent;
        talk_background.SetActive(istransparent);
        perform_back.color = currentColor;
    }

    private void SetMid(bool needmid)
    {
        Debug.Log("setting" + needmid);
        if ((isMid && needmid) || (!isMid && !needmid))
        {
            return;
        }
        if (needmid)
        {
            box_talk.rectTransform.position = center.position;
            box_talk.rectTransform.sizeDelta = new Vector2(box_talk.rectTransform.sizeDelta.x, 250);
            isMid = true;
        }
        else
        {
            box_talk.rectTransform.position = origin.position;
            box_talk.rectTransform.sizeDelta = new Vector2(box_talk.rectTransform.sizeDelta.x, 60);
            isMid = false;
        }
    }

    /// <summary>
    /// 默认跳转到下一个剧情
    /// </summary>
    /// <param name="need_refresh"></param>
    private void DialogueEnd(bool need_refresh = true)
    {
        Debug.Log("Is current waiting |||" + need_refresh);

        SuperController.Instance.PlayerRestore();
        SuperController.Instance.PauseStop();

        box_talk.text = "";
        active_trigger_num = "n";
        EndTalk(need_refresh);
    }

}