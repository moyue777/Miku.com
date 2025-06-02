using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpriteChanger : MonoBehaviour
{
    private static SpriteChanger _instance;
    public static SpriteChanger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SpriteChanger>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SpriteChanger");
                    _instance = obj.AddComponent<SpriteChanger>();
                }
            }
            return _instance;
        }
    }

    public List<SpriteManage> spriteManages = new List<SpriteManage>();
    [SerializeField]
    private Condition condition;
    private bool isEmergency;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        spriteManages = new List<SpriteManage>(GetComponentsInChildren<SpriteManage>());
    }

    void OnEnable()
    {
        EnsureEventSystemExists();
    }

    private void EnsureEventSystemExists()
    {
        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            GameObject eventSystemObject = new GameObject("EventSystem");
            eventSystem = eventSystemObject.AddComponent<EventSystem>();

            // 添加 StandaloneInputModule
            eventSystemObject.AddComponent<StandaloneInputModule>();

            // 确保至少有一个 Canvas 有 GraphicRaycaster
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            if (canvases.Length == 0)
            {
                Debug.LogWarning("No Canvas found in the scene. Please add a Canvas to ensure UI input works correctly.");
            }
            else
            {
                foreach (Canvas canvas in canvases)
                {
                    if (canvas.GetComponent<GraphicRaycaster>() == null)
                    {
                        canvas.gameObject.AddComponent<GraphicRaycaster>();
                    }
                }
            }
        }
    }
    public Condition GetCondition()
    {
        return condition;
    }
}