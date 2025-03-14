using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitGame()
    {
        #if UNITY_EDITOR
            // 如果我们在Unity编辑器中运行，则不退出，而是断开播放模式
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // 在实际构建的游戏中，退出游戏
            Application.Quit();
        #endif
    }
}