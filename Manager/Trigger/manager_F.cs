using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 天竺到其他场景的触发器
/// </summary>
public class manager_F : MyTrigger
{
    public PlotManager plotManager;
    private Vector2 targetPos;
    public string sceneName;
    public string AdName;

    void Start()
    {
        plotManager = PlotManager.Instance;
        targetPos = SceneJumpData.GetJumpVector(sceneName + AdName);
    }
    protected override void OnFKeyPressed()
    {
        if(plotManager != null)
        {
            plotManager.RefreshScene();
            SuperController.Instance.SetTarget(targetPos);
        }
        base.OnFKeyPressed();

        SceneManager.LoadScene(sceneName);   
    }
}