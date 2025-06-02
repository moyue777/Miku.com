using UnityEngine;

public class Timer : MonoBehaviour
{
    public float duration = 30f; // 总时间
    public float generate_threshold = 5f; // 调用方法的阈值

    private float timer; // 当前计时
    private bool isRunning = false; // 计时器是否正在运行
    private float lastThresholdTime; // 上一次调用方法的时间

    protected virtual void Start()
    {
        timer = 0f;
        isRunning = false;
        lastThresholdTime = 0f;
    }

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                Debug.Log("Time is up");
                TimeUp();
                StopTimer();
            }
            else if (timer - lastThresholdTime >= generate_threshold)
            {
                lastThresholdTime = timer;
                RepeatedAction();
            }
        }
    }

    /// <summary>
    /// 开始计时
    /// </summary>
    public virtual void StartTimer()
    {
        timer = 0f;
        lastThresholdTime = 0f;
        isRunning = true;
    }

    /// <summary>
    /// 停止计时
    /// </summary>
    public virtual void StopTimer()
    {
        isRunning = false;
    }

    /// <summary>
    /// 计时结束的回调方法
    /// </summary>
    public virtual void TimeUp()
    {
        // 在子类中重写此方法以实现具体逻辑
        Debug.Log("Timer has reached the duration.");
    }

    /// <summary>
    /// 每隔指定时间调用的方法
    /// </summary>
    public virtual void RepeatedAction()
    {
        // 在这里实现你希望每隔一段时间调用的逻辑
        Debug.Log("Repeated action called at " + Time.time);
    }

    public void OnDestroy()
    {
        StopTimer();
    }
}