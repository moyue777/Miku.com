using UnityEngine;

public class NextManage : MonoBehaviour
{
    public delegate void PlayerNext();
    private PlayerNext callNext;
    public void SetAction(PlayerNext action)
    {
        callNext = action;
    }
    // 当鼠标左键点击物体时调用
    void OnMouseDown()
    {
        if (callNext != null)
        {
            callNext(); // 调用指定方法
        }

    }
}
