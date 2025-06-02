using UnityEngine;
/// <summary>
/// Menu下的子Canva的基类
/// </summary>
public class UIButtonManage : MonoBehaviour
{
    public GameObject MenuCanva;
    protected virtual void Start()
    { }
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("now escaping");
            gameObject.SetActive(false);
            MenuCanva.SetActive(true);
        }
    }
}
