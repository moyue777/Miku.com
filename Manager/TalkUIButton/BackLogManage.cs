using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BackLogManage : MonoBehaviour
{
    public Canvas talkCanvas;
    public Canvas backLogCanvas;
    public GameObject backlog;
    public GameObject backlog_prefab;

    public void SetBackLog(List<Dialogue> backlog_contents)
    {
        for (int i = 0; i < backlog_contents.Count; i++)
        {
            GameObject cur_backlog = Instantiate(backlog_prefab, backlog.transform); // 设置父对象为 backlog
            cur_backlog.GetComponentInChildren<TMP_Text>().text = backlog_contents[i].text;
        }
    }

    public void ClearBackLog()
    {
        // 遍历并销毁所有子物体
        for (int i = backlog.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(backlog.transform.GetChild(i).gameObject);
        }
    }
}