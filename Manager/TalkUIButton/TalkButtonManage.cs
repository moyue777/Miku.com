using UnityEngine;
using UnityEngine.UI;

public class TalkButtonManage : MonoBehaviour
{
    public Canvas backlogCanvas;
    public Canvas currentCanvas;
    public PlotManager plotManager;
    public BackLogManage backLogManage;
    public Button jumpButton;//跳过键
    public Button nextManage;
    private bool backlogging;
    private bool needNext = false;
    void Start()
    {
        backlogCanvas.enabled = false;
        backlogging = false;
        plotManager = PlotManager.Instance;
        nextManage.onClick.AddListener(() =>
        {
            needNext = true;
        });
        jumpButton.onClick.AddListener(() =>
        {
            plotManager.jump();
        });
    }
    void Update()
    {
        if (plotManager.superController.isTalking == true)

        {
            // 检测鼠标滚轮输入
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0 && !backlogging)
            {
                // 鼠标滚轮向上滚动
                backlogging = true;
                Backlog();
            }
            else if (scroll < 0 || (needNext && !backlogging))
            {
                if (backlogging)
                {
                    backlogging = false;
                    backlogCanvas.enabled = false;
                    backLogManage.ClearBackLog();
                }
                else
                {
                    // 鼠标滚轮向下滚动
                    NextDialogue();
                }

            }
        }
    }
    private void NextDialogue()
    {
        needNext = false;
        plotManager.ClickNext();
    }
    private void Backlog()
    {
        backlogCanvas.enabled = true;
        backLogManage.SetBackLog(plotManager.GetBacklogs());

    }
}