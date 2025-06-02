using UnityEngine;

public abstract class PressFacility : MonoBehaviour, Iinteract
{
    protected virtual void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键单击
        {
            Handle();
        }
    }

    public virtual void Handle()
    {
        // 在子类中重写此方法以实现具体逻辑
        Debug.Log("Left click detected on " + gameObject.name);
    }

    public virtual void Recieve()
    {
        // 在子类中重写此方法以实现具体逻辑
        Debug.Log("Received input on " + gameObject.name);
    }

    public virtual void Recieve(int count)
    {}
}