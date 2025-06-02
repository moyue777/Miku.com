using UnityEngine;

public abstract class DragFacility : MonoBehaviour, Iinteract
{
    public float minDistance = 10f; // 最小移动距离
    public int minDragCount = 3; // 最小拖动次数

    private Vector3 lastMousePosition;

    protected virtual void OnMouseDown()
    {

        lastMousePosition = Input.mousePosition;
    }

    protected virtual void OnMouseDrag()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        float distance = Vector3.Distance(lastMousePosition, currentMousePosition);

        if (distance > minDistance)
        {
            lastMousePosition = currentMousePosition;
            Handle();
        }
    }

    public virtual void Handle()
    {
        // 在子类中重写此方法以实现具体逻辑
        Debug.Log("Drag threshold exceeded on " + gameObject.name);
    }
    public virtual void Recieve()
    {
        Debug.Log("Received input on " + gameObject.name);
    }
    public virtual void Recieve(int count)
    {}

}