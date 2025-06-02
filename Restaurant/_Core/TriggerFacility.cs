using UnityEngine;

public abstract class TriggerFacility : MonoBehaviour, Iinteract
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        // 检测是否有其他物体进入当前物体的触发器区域
        Debug.Log("Object entered: " + other.gameObject.name);
        if (other.CompareTag("Customer")) 
        {
            Debug.Log("find customer");
            Handle();
        }
    }

    public virtual void Handle()
    {
        // 在子类中重写此方法以实现具体逻辑
        Debug.Log("HandleEnter method called for " + gameObject.name);
    }

    public virtual void Recieve()
    {
        // 在子类中重写此方法以实现具体逻辑
        Debug.Log("Recieve method called for " + gameObject.name);
    }
    public virtual void Recieve(int count)
    {}
}