using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 控制UI切换和场景加载的回调
/// </summary>
public partial class SuperController
{
    private AsyncOperation asyncLoad;
    /// <summary>
    /// 用于指示玩家位置设置的消息队列
    /// </summary>
    private List<Vector3> vector3s = new List<Vector3>();
    private string sceneName;
    private bool needWork;
    private bool needSleep;
    public void Set_needWork(bool needWork = true)
    {
        this.needWork = needWork;
    }
    public void Set_needSleep(bool needSleep = true)
    {
        this.needSleep = needSleep;
    }

    /// <summary>
    /// 检查并处理消息队列,来设置玩家位置
    /// </summary>
    void CheckQueue()
    {
        if (vector3s.Count > 0)
        {
            PlayerControl playerControl = FindObjectOfType<PlayerControl>();
            if (playerControl != null)
            {
                playerControl.gameObject.transform.position = vector3s[0];
                Debug.Log("truely setting" + vector3s[0]);
                vector3s.Clear();
            }
        }

    }

    /// <summary>
    /// 禁止玩家移动，时间不静止的伪暂停
    /// </summary>
    public void PlayerStop()
    {
        PlayerControl playerControl = FindObjectOfType<PlayerControl>();
        playerControl.Set_canMove(false);
    }

    /// <summary>
    /// 恢复玩家移动，用于结束对话
    /// </summary>
    public void PlayerRestore()
    {
        PlayerControl playerControl = FindObjectOfType<PlayerControl>();
        playerControl.Set_canMove(true);
    }

    /// <summary>
    /// 异步加载新场景
    /// </summary>
    /// <param name="targetName"></param>
    /// <param name="delay"></param>
    public void LoadNewScene(string targetName, float delay)
    {
        sceneName = targetName;
        staticUIManage.Activate();
        StartCoroutine(LoadScene(targetName, delay));
    }


    /// <summary>
    /// 异步所用携程，暂时仅起始页使用
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator LoadScene(string sceneName, float delay)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
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
        OnLoaded(scene.name);
    }

    /// <summary>
    /// 通用回调
    /// </summary>
    public void OnLoaded(string sceneName)
    {
        Debug.Log("loaded" + sceneName);

        CloseSystem(true);
        //加载对话 trigger
        LoadTrigger(sceneName);

        //玩家位置
        CheckQueue();
        PlayerControl playerControl = FindObjectOfType<PlayerControl>();

        //摄像机
        if (playerControl != null)
        {
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.SetTarget(playerControl.gameObject.transform);
            }
        }
    }

    /// <summary>
    /// 加载triggers
    /// </summary>
    private void LoadTrigger(string sceneName)
    {
        if (!(needWork) && !(needSleep))
        {
            List<string> triggers = SceneTriggers.GetSceneTriggers(sceneName);

            Debug.Log("loading triggers");

            foreach (var item in plotManager.current_plotdata.updateList.active_triggers)
            {
                Debug.Log("checking" + item);
                if (triggers.Contains(item))
                {
                    GameObject NPC = Resources.Load<GameObject>("NPC_Talk/" + item);
                    if (NPC != null)
                    { GameObject obj = Instantiate(NPC); }
                    else
                    {
                        Debug.Log("NPC not found");
                    }
                }
            }

        }
    }
}