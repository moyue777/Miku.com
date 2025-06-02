using UnityEngine;
public class TeaPot : PressFacility
{
    private bool isWaterReady = false;
    private bool isTeaReady = false;
    public bool CheckWater()
    {return isWaterReady;}
    public bool CheckTea()
    {return isTeaReady;}

    public void WaterReady()
    {
        isWaterReady = true;
    }

    public void TeaReady()
    {
        isTeaReady = true;
    }
    
    public override void Handle()
    {
        if (isWaterReady && isTeaReady)
        {
            isWaterReady = false;
            isTeaReady = false;

            Debug.Log("all is ready!");
        }   
        else
        {
            
        }
    }

    public override void Recieve(int count)
    {
        
    }
}