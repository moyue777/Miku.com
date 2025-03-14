using UnityEngine;

/// <summary>
/// 仅管理加载Cook的数据
/// </summary>
public partial class SuperController : MonoBehaviour
{
    public void SetCookData()
    {

    }

    public CookData GetCookData()
    {
        return playerData.cookData;
    }
}