using UnityEngine;
public class StartUIManager : MonoBehaviour
{
    public Canvas canvas;
    public ShiftUIControl left;
    public ShiftUIControl right;
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.SetActive(false);
    }
    public void Activate()
    {
        canvas.gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        canvas.gameObject.SetActive(false);
    }

    public void Reset()
    {
        left.Reset();
        right.Reset();
    }
}