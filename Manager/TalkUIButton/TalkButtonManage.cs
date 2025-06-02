using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TalkButtonManage : MonoBehaviour
{
    [Header("Progress UI")]
    public Image progressFill; // 环形进度条Image组件
    public float fillSpeed = 0.02f; // 填充速度（与FixedUpdate频率匹配）

    public Canvas backlogCanvas;
    public Canvas currentCanvas;
    public PlotManager plotManager;
    public BackLogManage backLogManage;
    private bool backlogging;
    private float fKeyHoldTime;
    private float fKeyHoldThreshold = 2f;

    void Start()
    {
        fKeyHoldTime = 0f;
        backlogCanvas.enabled = false;
        backlogging = false;
        plotManager = PlotManager.Instance;

        if (progressFill != null)
        {
            progressFill.type = Image.Type.Filled;
            progressFill.fillMethod = Image.FillMethod.Radial360;
            progressFill.fillAmount = 0;
        }
    }

    void Update()
    {
        if (SuperController.Instance.isTalking == true)
        {
            // 检测鼠标滚轮输入
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0 && !backlogging)
            {
                // 鼠标滚轮向上滚动
                backlogging = true;
                Backlog();
            }
            else if (!backlogging && (scroll < 0 || Input.GetMouseButtonDown(0)))
            {
                NextDialogue();
            }
            else if (backlogging && scroll < 0)
            {
                backlogging = false;
                backlogCanvas.enabled = false;
                backLogManage.ClearBackLog();
            }
        }
    }

    void FixedUpdate()
    {
        if (SuperController.Instance.isTalking == true)
        {
            if (Input.GetKey(KeyCode.F))
            {
                fKeyHoldTime += 0.02f;
                Debug.Log("F键被按住" + fKeyHoldTime);
                UpdateProgressVisual(Mathf.Clamp01(fKeyHoldTime / fKeyHoldThreshold));

                if (fKeyHoldTime >= fKeyHoldThreshold)
                {
                    DialogueJump();
                    ResetProgress();
                }
            }
            else
            {
                ResetProgress(); // 如果F键未被按住，重置计时器
            }
        }
    }
    void UpdateProgressVisual(float progress)
    {
        if (progressFill != null)
        {
            // 平滑过渡填充效果
            progressFill.fillAmount = Mathf.Lerp(progressFill.fillAmount, progress, 10 * Time.fixedDeltaTime);

            // 动态颜色变化（可选）
            progressFill.color = Color.Lerp(Color.white, Color.yellow, progress);
        }
    }

    void ResetProgress()
    {
        fKeyHoldTime = 0f;
        if (progressFill != null)
        {
            progressFill.fillAmount = 0;
        }
    }
    private void NextDialogue()
    {
        plotManager.ClickNext();
    }

    private void Backlog()
    {
        backlogCanvas.enabled = true;
        backLogManage.SetBackLog(plotManager.GetBacklogs());
    }

    private void DialogueJump()
    {
        plotManager.jump();
    }
}