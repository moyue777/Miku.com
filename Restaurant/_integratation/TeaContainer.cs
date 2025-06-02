using UnityEngine;

public class TeaContainer: MidContainer
{
    protected override void Manipulate()
    {
        if (teaPot.CheckTea() == false)
        {
            count--;
            teaPot.TeaReady();
        }
    }
}