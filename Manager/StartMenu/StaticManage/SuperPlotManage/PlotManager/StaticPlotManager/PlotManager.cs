using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*****************
*PlotManager 管理trigger和 TalkUIControl发送文本 
*数据层
*/
public partial class PlotManager : MonoBehaviour
{
    public static PlotManager Instance; // 自己的单例
    public SuperController superController;

    private int plot_stage;//当前剧情阶段
    private Dictionary<int, PlotData> triggers = new Dictionary<int, PlotData>(){

    };//其他的短暂对话
    private int waiting_trigger_num;//当前希望得到回答的监听

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
        current_string_num = 0;

        //Super control
        superController = FindObjectOfType<SuperController>();
        plot_stage = superController.playerData.PlotStage;
    }

    //触发器调用,发送文本开始对话
    public void TriggerSend(int active_trigger_num)
    {
        if (active_trigger_num == waiting_trigger_num)
        {
            SetDialogueText(GetDialogue("0"));
        }
    }

    //按钮调用,talkuicontrol结束
    public void EndTalk()
    {
        if (triggers != null)
        {
            // int mid = triggers[waiting_trigger_num].waiting_trigger;
            // UpdatePlotStage(triggers[waiting_trigger_num].updateList.target);
            // waiting_trigger_num = mid;
        }
        talk_canva.enabled = false;
    }

    //更新节点
    private void UpdatePlotStage(Dictionary<int, string> target)
    {
        if (target != null)
        {
            foreach (var i in target)
            {
                if (triggers.ContainsKey(i.Key))
                {
                    triggers[i.Key] = GetDialogue(i.Value);
                }
            }
        }
    }

    //加载对话总工具
    private PlotData GetDialogue(string plot_name)
    {
        string json = LoadJsonFromResources($"Plots/{plot_name}_plot");
        if (json != null)
        {
            PlotData plotData = ParseJsonToPlotData(json);
            // 处理加载的对话数据
            return plotData;
        }
        else
        { return null; }
    }

    //工具 加载剧情
    private string LoadJsonFromResources(string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset != null)
        {
            return textAsset.text;
        }
        else
        {
            Debug.LogError($"Failed to load JSON file: {fileName}");
            return null;
        }
    }

    //工具  加载剧情
    private PlotData ParseJsonToPlotData(string json)
    {
        return JsonUtility.FromJson<PlotData>(json);
    }
}