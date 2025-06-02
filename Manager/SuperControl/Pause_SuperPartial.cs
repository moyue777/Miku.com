using UnityEngine;

/// <summary>
/// 仅管理暂停
/// </summary>
public partial class SuperController
{
    private int ispaused = 0;
    /// <summary>
    /// 时间停止，暂停计数器加一
    /// </summary>
    public void PauseStart()
    {
        ispaused++;
        Check();
    }

    /// <summary>
    /// 时间停止，暂停计数器减一
    /// </summary>
    public void PauseStop()
    {
        if (ispaused > 0)
        { ispaused--; }
        Check();
    }

    /// <summary>
    /// 检测时停状态，并设置时间
    /// </summary>
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

    /// <summary>
    /// 唤起伪暂停UI
    /// </summary>
    public void CallSystem(bool is_save = false)
    {
        pauseCanva.SetActive(true);
        if (is_save)
        {
            pauseCanva_system.SetActive(false);
            pauseCanva_save.SetActive(true);
        }
    }

    public void CloseSystem(bool is_save = false)
    {
        pauseCanva.SetActive(false);
        if (is_save)
        {
            pauseCanva_save.SetActive(false);
            pauseCanva_system.SetActive(true);
        }
    }

}