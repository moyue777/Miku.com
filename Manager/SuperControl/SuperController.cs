using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public partial class SuperController : MonoBehaviour
{
    public static SuperController Instance;
    public Dictionary<string, GameObject> triggers_prefabs;//triggers预制体
    public Dictionary<string, List<string>> triggers_locoation = new Dictionary<string, List<string>>();//triggers的位置

    public PlayerData playerData;//玩家数据
    
    public GameObject SelfObject;
    public bool isTalking = false;
    private Vector2 Target;
     public PlotManager plotManager;
    
    public void SetTarget(Vector2 target)
    {
        vector3s.Add(target);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(SelfObject);
        }
        else
        {
            Destroy(SelfObject);
        }

        string location = "NPC_Talk/";
        triggers_prefabs = new Dictionary<string, GameObject>()
        {
            {"male_man_character",Resources.Load<GameObject>(location + "male_man_character")},
            {"female_main_character",Resources.Load<GameObject>(location + "female_main_character")},
        };
        triggers_locoation = new Dictionary<string, List<string>>()
        {
            {"HomeScene",new List<string>(){
                "female_main_character",
                }
            },
            {"StreetScene",new List<string>(){
                "male_man_character",
            }}
        };
    }

    void Start()
    {
        plotManager = FindObjectOfType<PlotManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void ChangeTalking(bool Talking)
    {
        isTalking = Talking;
    }

    public void FinishTalk()
    {
        Debug.Log("before" + playerData.PlotStage);
        playerData.PlotStage = plotManager.current_plotdata.next_id;
        Debug.Log("after" + playerData.PlotStage);
        plotManager.UpdateList();
    }
}