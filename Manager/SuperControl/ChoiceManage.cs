using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class ChoiceManage : MonoBehaviour
{
    public GameObject choiceButton;
    public Transform startPos;
    public List<GameObject> choose_buttons = new List<GameObject>();
    private int  maxCount = 3;
    public void Start()
    {
        for (int i = 0; i < maxCount; i++)
        {   
            GameObject temp = Instantiate(choiceButton);
            temp.transform.SetParent(gameObject.transform);
            temp.transform.position = startPos.position + new Vector3(0, -i * 50, 0);
            int curIndex = i;
            temp.GetComponent<Button>().onClick.AddListener(()=>{
                buttonclick(curIndex);
            });
            temp.SetActive(false);
            choose_buttons.Add(temp);
        }
    }
    public void buttonclick(int index)
    {
        Debug.Log(index);
        PlotManager.Instance.OnOptionClick(index);
        for (int i = 0 ; i < maxCount; i++)
        {
            choose_buttons[i].SetActive(false);
            choose_buttons[i].GetComponentInChildren<Text>().text = "";
        }
    }
    public void InitialChoice(int num, List<string> choices, bool active= true)
    {
        for (int i = 0 ; i < num; i++)
        {
            choose_buttons[i].SetActive(active);
            choose_buttons[i].GetComponentInChildren<Text>().text = choices[i];
        }
    }

}