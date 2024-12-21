using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public string sceneName;

    public void NewGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }
    
    
    
}