using UnityEngine;

public class ShiftUIControl : MonoBehaviour
{
    public Transform shiftui;
    public Transform targettransform;
    private Vector3 origin;
    public float moveSpeed = 3.0f; // 移动速度

    // Start is called before the first frame update
    void Start()
    {
        origin = shiftui.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveShiftUI();
    }

    private void MoveShiftUI()
    {
        // 使用 Vector3.MoveTowards 实现匀速运动
        shiftui.position = Vector3.MoveTowards(shiftui.position, targettransform.position, moveSpeed * Time.deltaTime);
    }

    public void Reset()
    {
        if (origin == null)
        {
            Debug.Log("shiftui is null");
        }
        shiftui.position = origin;
    }
}