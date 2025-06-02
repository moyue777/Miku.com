using UnityEngine;
public class WaterBoiler : HoldFacility
{
    public Iinteract kettle;
    private int waterCount = 0;
    private int waterCount_max = 10;
    protected override void Update()
    {
        if (isHolding)
        {
            holdDuration += Time.deltaTime;
            Perform();

            if (holdDuration >= holdThreshold)
            {
                Handle();
                isHolding = false; // 重置按住状态
                holdDuration = 0;
            }
        }
        
    }

    private void Perform()
    {
        
    }

    public override void Handle()
    {
        waterCount = waterCount_max;
        Debug.Log("watercount is " + waterCount);
        kettle.Recieve();
    }
}