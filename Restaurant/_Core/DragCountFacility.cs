using UnityEngine;

public abstract class DragCountFacility : MonoBehaviour, Iinteract
{
    public float minDistance = 100f; // 最小移动距离
    public int minDragCount = 20; // 最小拖动次数

    private Vector3 lastMousePosition;
    private int dragCount = 0;

    protected virtual void OnMouseDown()
    {
        lastMousePosition = Input.mousePosition;
        dragCount = 0;
    }

    protected virtual void OnMouseDrag()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        float distance = Vector3.Distance(lastMousePosition, currentMousePosition);

        if (distance > minDistance)
        {
            Debug.Log("Drag current count" + dragCount);
            dragCount++;
            lastMousePosition = currentMousePosition;

            if (dragCount >= minDragCount)
            {
                Handle();
                dragCount = 0; // 重置计数器
            }
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