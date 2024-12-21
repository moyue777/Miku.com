using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public partial class SuperController
{
    public StatciUIManager staticUI;
    private AsyncOperation asyncLoad;

    public void LoadNewScene(string sceneName, float delay)
    {
        StartCoroutine(LoadScene(sceneName, delay));
    }

    IEnumerator LoadScene(string sceneName, float delay)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        Debug.Log("StaticUI.Acitivate");
        staticUI.Activate();

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
        OnSceneLoaded(); // 调用加载完成后的回调
    }

    private void OnSceneLoaded()
    {
        staticUI.Deactivate();   
        staticUI.Reset();
    }
}