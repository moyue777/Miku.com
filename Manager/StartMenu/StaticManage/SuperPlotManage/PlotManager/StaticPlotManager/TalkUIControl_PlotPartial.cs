using UnityEngine;
using TMPro;
using System.Collections;
/***********************
*TalkUIControl 仅管理在对话页面加载对话,对话文本从PlotManager获得
*/
public partial class PlotManager
{
    public Canvas talk_canva;//对话canvas
    public TMP_Text target_talk;//对话文本框
    public PlotData current_plotdata;//当前剧情

    private string current_string;//当前应该输出的对话
    private int current_string_num;//当前应该输出的对话的index
    private Coroutine textUpdateCoroutine;//加载文本的协程
    
    public void RefreshScene()
    {
        talk_canva.enabled = false;
        DialogueEnd();
    }

    //外部调用，开始对话 
    public void SetDialogueText(PlotData dialogue_text)
    {
        SuperController.Instance.PauseStart();
        current_string_num = 0;
        current_plotdata = dialogue_text;
        talk_canva.enabled = true;
        ShowText();
    }

    //获得当前应该输出的文本
    private string GetCurrentString()
    {
        if (current_string_num >= current_plotdata.dialogues.Count)
        {
            DialogueEnd();
            return null;
        }
        return current_plotdata.dialogues[current_string_num++].text;
    }

    private void DialogueEnd()
    {
        SuperController.Instance.PauseStop();
        target_talk.text = "";
        EndTalk();
    }
    //开始显示下一句对话
    private void ShowText()
    {
        Debug.Log("start a new coroutine");
        if (textUpdateCoroutine != null)
        {
            StopCoroutine(textUpdateCoroutine);
        }
        textUpdateCoroutine = StartCoroutine(UpdateTextCoroutine());
    }

    //外部调用,向下键
    public void ClickNext()
    {
        if (textUpdateCoroutine != null)
        {
            StopCoroutine(textUpdateCoroutine);
            textUpdateCoroutine = null;
            target_talk.text = current_string; // 清空文本
        }
        else
        {
            ShowText();
        }
    }

    //用于显示文本的协程
    private IEnumerator UpdateTextCoroutine()
    {
        current_string = GetCurrentString();
        if (current_string == null)
        {
            yield break;
        }
        target_talk.text = "";
        foreach (char c in current_string.ToCharArray())
        {
            target_talk.text += c;
            yield return new WaitForSecondsRealtime(0.1f); // 调整延迟时间以控制显示速度
        }
        textUpdateCoroutine = null; // 协程结束后将变量设为null
    }
}