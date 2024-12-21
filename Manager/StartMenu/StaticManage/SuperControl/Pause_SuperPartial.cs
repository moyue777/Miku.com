using UnityEngine;

public partial class SuperController
{
    private int ispaused = 0;
    public void PauseStart()
    {
        ispaused++;
        Check();
    }

    public void PauseStop()
    {
        if (ispaused > 0)
        { ispaused--; }
        Check();
    }

    public void Check()
    {
        if (ispaused != 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}