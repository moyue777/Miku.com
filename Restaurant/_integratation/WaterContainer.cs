using UnityEngine;

public class WaterContainer: MidContainer
{
    protected override void Manipulate()
    {
        if(teaPot.CheckWater() == false)
        {
            count--;
            teaPot.WaterReady();
        }
    }
}