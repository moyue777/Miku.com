using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 控制UI切换和场景加载的回调
/// </summary>
public partial class SuperController
{
    public StartUIManager staticUI;
    private AsyncOperation asyncLoad;
    private List<Vector3> vector3s = new List<Vector3>();
    private string sceneName;

    /// <summary>
    /// 检查并处理消息队列,来设置玩家位置
    /// </summary>
    void CheckQueue()
    {
        if (vector3s.Count > 0)
        {
            PlayerControl playerControl = FindObjectOfType<PlayerControl>();
            playerControl.gameObject.transform.position = vector3s[0];
            Debug.Log("truely setting" + vector3s[0]);
            vector3s.Clear();
        }

    }

    /// <summary>
    /// 异步加载新场景
    /// </summary>
    /// <param name="targetName"></param>
    /// <param name="delay"></param>
    public void LoadNewScene(string targetName, float delay)
    {
        sceneName = targetName;
        staticUI.Activate();
        StartCoroutine(LoadScene(targetName, delay));
    }
    IEnumerator LoadScene(string sceneName, float delay)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        Debug.Log("StaticUI.Acitivate");


        // 加载完成前，等待指定的延迟时间
        yield return new WaitForSeconds(delay);
        while (!asyncLoad.isDone)
        {
            // Debug.Log(asyncLoad.progress);
            // progressBar.value = asyncLoad.progress; // 更新进度条
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    /// <summary>
    /// 注册的回调函数
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("now call back");
        OnLoaded();
    }
    public void OnLoaded()
    {
        CheckQueue();
        PlayerControl playerControl = FindObjectOfType<PlayerControl>();
        if (playerControl != null)
        {
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.SetTarget(playerControl.gameObject.transform);
            }
        }
    }
}