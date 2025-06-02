using UnityEngine;
using UnityEngine.SceneManagement;
public partial class SuperController : MonoBehaviour
{
    public static SuperController Instance;
    public GameObject SelfObject;
    [Header("Pause Canva")]
    public GameObject pauseCanva;
    public GameObject pauseCanva_system;
    public GameObject pauseCanva_save;
    [Header("Manage")]
    public StartUIManager staticUIManage;
    public PlotManager plotManager;
    [Header("player information")]
    public PlayerData playerData;//玩家数据
    public bool isTalking = false;
    [SerializeField]
    private Condition condition = Condition.Day;
    public Condition GetCondition()
    {
        return condition;
    }

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
        { Destroy(SelfObject); }
    }
    void Start()
    {
        plotManager = FindObjectOfType<PlotManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        pauseCanva.SetActive(false);
    }
    /// <summary>
    /// 仅仅改变标志位
    /// </summary>
    /// <param name="Talking"></param>
    public void ChangeTalking(bool Talking)
    {
        isTalking = Talking;
    }

    /// <summary>
    /// 正确的trigger触发，SuperController结束
    /// </summary>
    public void FinishTalk()
    {
        Debug.Log("before" + playerData.PlotStage);
        playerData.PlotStage = plotManager.current_plotdata.next_id;
        Debug.Log("after" + playerData.PlotStage);
        plotManager.UpdateList();
    }
}