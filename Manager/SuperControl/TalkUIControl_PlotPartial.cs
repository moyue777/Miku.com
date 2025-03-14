using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 处理对话过程的工具
/// </summary>
public partial class PlotManager : MonoBehaviour
{
    public Canvas talk_canva; // 对话canvas
    public Canvas back_canva;//backlog——canvas
    public Text target_talk; // 对话文本框
    public AudioSource audioSource; // 添加音频源组件

    public PlotData current_plotdata; // 当前剧情
    public List<Dialogue> current_dialogues;

    private string active_trigger_num;
    private Dialogue out_dialogue; // 当前应该输出的对话
    private int current_string_num; // 当前应该输出的对话的index
    private Coroutine textUpdateCoroutine; // 加载文本的协程

    public void RefreshScene()
    {
        talk_canva.enabled = false;
        DialogueEnd(false);
    }

    /// <summary>
    /// 外部调用，初始化对话 
    /// </summary>
    /// <param name="current_trigger_num">能够推动剧情的trigger</param>
    public void SetDialogueText(string current_trigger_num)
    {
        SuperController.Instance.PauseStart();
        current_string_num = 0;
        talk_canva.enabled = true;
        active_trigger_num = current_trigger_num;

        foreach (var scripts in current_plotdata.scripts)
        {
            if (scripts.names == active_trigger_num)
            {
                current_dialogues = scripts.dialogues;
                break;
            }
        }

        ShowText();
    }

    /// <summary>
    /// 对话页面调用，跳过
    /// </summary>
    public void jump()
    {
        DialogueEnd(active_trigger_num == current_plotdata.waiting_trigger ? false : true);
    }

    /// <summary>
    /// 对话页面调用，下一句
    /// </summary>
    public void ClickNext()
    {
        if (textUpdateCoroutine != null)
        {
            StopCoroutine(textUpdateCoroutine);
            textUpdateCoroutine = null;
            target_talk.text = out_dialogue.text; // 清空文本
        }
        else
        {
            ShowText();
        }
    }

    /// <summary>
    /// 对话页面调用，获取backlog
    /// </summary>
    /// <returns></returns>
    public List<Dialogue> GetBacklogs()
    {
        List<Dialogue> backlogs = new List<Dialogue>();
        for (int i = 0 ; i < current_string_num; i ++)
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
            DialogueEnd(active_trigger_num == current_plotdata.waiting_trigger ? false : true);
            return null;
        }
        return current_dialogues[current_string_num++];
    }


    /// <summary>
    /// 开始显示下一句对话
    /// </summary>
    private void ShowText()
    {
        Debug.Log("start a new coroutine");
        if (textUpdateCoroutine != null)
        {
            StopCoroutine(textUpdateCoroutine);
        }
        textUpdateCoroutine = StartCoroutine(UpdateTextCoroutine());
    }

    /// <summary>
    /// 默认跳转到下一个剧情
    /// </summary>
    /// <param name="need_refresh"></param>
    private void DialogueEnd(bool need_refresh = true) 
    {
        SuperController.Instance.PauseStop();
        target_talk.text = "";
        active_trigger_num = "n";
        EndTalk(need_refresh);
    }
    
    /// <summary>
    /// 用于显示文本，播放音频的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateTextCoroutine()
    {
        out_dialogue = GetCurrentDialogue();
        if (out_dialogue == null)
        {
            yield break;
        }
        target_talk.text = "";
        LoadCharacter(out_dialogue.character);
        LoadVoice(out_dialogue.voice);

        // 播放音频
        if (audioSource != null && current_audioClip != null)
        {
            audioSource.clip = current_audioClip;
            audioSource.Play();
        }

        foreach (char c in out_dialogue.text.ToCharArray())
        {
            target_talk.text += c;
            yield return new WaitForSecondsRealtime(0.1f); // 调整延迟时间以控制显示速度
        }
        textUpdateCoroutine = null; // 协程结束后将变量设为null
    }
}