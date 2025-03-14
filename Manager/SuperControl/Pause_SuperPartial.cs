using UnityEngine;

/// <summary>
/// 仅管理暂停
/// </summary>
public partial class SuperController
{
    private int ispaused = 0;
    /// <summary>
    /// 开始暂停，暂停计数器加一
    /// </summary>
    public void PauseStart()
    {
        ispaused++;
        Check();
    }
    
    /// <summary>
    /// 结束暂停，暂停计数器减一
    /// </summary>
    public void PauseStop()
    {
        if (ispaused > 0)
        { ispaused--; }
        Check();
    }

    private void Check()
    {
        if (ispaused != 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}