using TMPro;
using UnityEngine;

public class Flag : MonoBehaviour
{
    // 存储所有的子物体
    public GameObject child;

    void Start()
    {   
        // 初始状态下将子物体设为不激活
        DeactivateChildren();
        if (gameObject.GetComponent<TeaWaitingProcess>() != null)
        {
            child.GetComponentInChildren<TMP_Text>().text = gameObject.GetComponent<TeaWaitingProcess>().ingredient;
        }else if (gameObject.GetComponent<Ingredient>() != null)
        {
            child.GetComponentInChildren<TMP_Text>().text = gameObject.GetComponent<Ingredient>().ingredientName;    
        }
        Debug.Log(child.GetComponentInChildren<TMP_Text>().text);
    }

    void OnMouseEnter()
    {
        Debug.Log("mouse entered");
        // 鼠标进入时激活所有子物体
        ActivateChildren();
    }

    void OnMouseExit()
    {
        // 鼠标移出时将所有子物体设为不激活
        DeactivateChildren();
    }

    void ActivateChildren()
    {
        child.SetActive(true);        
    }

    void DeactivateChildren()
    {
       child.SetActive(false);
    }
}