using System.Collections;
using UnityEngine;
public class StatciUIManager : MonoBehaviour
{
    public static StatciUIManager Instance { get;private set;}
    public Canvas canvas;
    public ShiftUIControl left;
    public ShiftUIControl right;
    void Start()
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
        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.SetActive(false);
    }
    public void Activate()
    {
        Debug.Log("call for setactive(true)");
        canvas.gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        Debug.Log("call for deactivate");
        canvas.gameObject.SetActive(false);
    }
    
    public void Reset()
    {
            left.Reset();
            right.Reset();
    }
}