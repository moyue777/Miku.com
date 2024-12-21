using UnityEngine;
public partial class SuperController : MonoBehaviour
{
    public static SuperController Instance;
    public PlayerData playerData;//玩家数据
    private int slot;
    private bool isLoading= false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}

