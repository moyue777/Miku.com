using UnityEngine;

public class MidContainer : PressFacility
{
    public TeaPot teaPot;
    [SerializeField]
    protected int count;
    [SerializeField]
    protected int countNesseary;
    [SerializeField]
    protected int countMax;
    public override void Handle()
    {
        base.Handle();
        if (count >= countNesseary)
        {
            Manipulate();
        }
    }

    public override void Recieve()
    {
        base.Recieve();
        if (count < countMax)
        {
            count ++;
            Debug.Log("Received teas, now" + count);
        }
    }

    public override void Recieve(int count)
    {
       base.Recieve(count);
    }

    protected virtual void Manipulate()
    {
        
    }
}