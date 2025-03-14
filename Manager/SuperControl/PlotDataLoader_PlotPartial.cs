using UnityEngine;
/// <summary>
/// 读取json的工具
/// </summary>
public partial class PlotManager : MonoBehaviour
{
    /// <summary>
    /// 加载当前PlayerData里的plotstage
    /// </summary>
    public void UpdateList()
    {
        string json = LoadJsonFromResources($"Plots/{superController.playerData.PlotStage}_plot");
        if (json != null)
        {
            PlotData plotData = ParseJsonToPlotData(json);
            // 处理加载的对话数据
            current_plotdata = plotData;
        }
    }

    /// <summary>
    /// 工具 加载json
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 工具  读取json
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    private PlotData ParseJsonToPlotData(string json)
    {
        return JsonUtility.FromJson<PlotData>(json);
    }
}