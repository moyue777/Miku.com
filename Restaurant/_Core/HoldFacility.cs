using UnityEngine;

public abstract class HoldFacility : MonoBehaviour, Iinteract
{
    public float holdThreshold = 1f; // 按住时间阈值（秒）
    protected float holdDuration;
    protected bool isHolding = false;

    protected virtual void OnMouseDown()
    {
        holdDuration = 0f; // 记录按下时间
        isHolding = true;
    }

    protected virtual void Update()
    {
        if (isHolding)
        {
            holdDuration += Time.deltaTime;
            if (holdDuration >= holdThreshold)
            {
                Handle();
                isHolding = false; // 重置按住状态
            }
        }
    }

    protected virtual void OnMouseUp()
    {
        isHolding = false; // 重置按住状态
    }

    public virtual void Handle()
    {
        // 在子类中重写此方法以实现具体逻辑
        Debug.Log("Hold threshold exceeded on " + gameObject.name);
    }
    
    public virtual void Recieve()
    {   
        Debug.Log("Received input on " + gameObject.name);
    }
    public virtual void Recieve(int count)
    {}
}