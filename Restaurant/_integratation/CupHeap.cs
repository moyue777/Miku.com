using UnityEngine;

public class CupHeap : PressFacility
{
    private bool hasCup;
    private CupData cupData;
    void Start()
    {
        hasCup = false;
        cupData = new CupData();
    }

    /// <summary>
    /// facility调用，加料
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void SetAttribute(string name, object value)
    {
        if (hasCup)
        { cupData.SetAttribute(name, value); }
    }

    /// <summary>
    /// facility调用，检查是否可以打包
    /// </summary>
    /// <param name="name"></param>
    public void GetAttribute(string name)
    {
        if (hasCup)
        { cupData.GetAttribute(name); }
    }

    /// <summary>
    /// 返回hasCup检查是否有杯子
    /// </summary>
    public bool CheckCup()
    {
        return hasCup;
    }

    public override void Handle()
    {
        base.Handle();
        if (!hasCup)
        {
            hasCup = true;
            cupData.ResetAttributes();
        }
    }

    /// <summary>
    /// cup调用，标志cup结束生命周期
    /// </summary>
    public override void Recieve()
    {
        hasCup = false;
    }
}