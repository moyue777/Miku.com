using UnityEngine;
using UnityEngine.SceneManagement;
public class manager_F : MyTrigger
{
    public PlotManager plotManager;
    public string sceneName;

    void Start()
    {
        plotManager = FindObjectOfType<PlotManager>();
    }
    protected override void OnFKeyPressed()
    {
        if(plotManager != null)
        {
            Debug.Log("开始跳转");
            plotManager.RefreshScene();
        }
        base.OnFKeyPressed();
        SceneManager.LoadScene(sceneName);   
    }
}