using System;
using System.Collections;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTalker : MonoBehaviour
{
    private bool entered;
    public Canvas talk_canva;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查进入触发器的是否是玩家
        if (other.CompareTag("Player"))
        {
            entered = true;
            Debug.Log("Player entered trigger area on " + gameObject.name);
            // 玩家进入触发区域，现在可以按F键加载场景
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered = false;
            Debug.Log("Player left trigger area on ");
            // 玩家离开触发区域，现在无法按F键加载场景
        }
    }

    void Start()
    {
    }
    void Update()
    {
        // 检测玩家是否按下了F键
        if (Input.GetKeyDown(KeyCode.F) && entered)
        {
            talk_canva.gameObject.SetActive(true);
        }
    }
    public void talk_close()
    {
        talk_canva.gameObject.SetActive(false);
    }
    
}