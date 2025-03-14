using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject catcher;
    public void Launch()
    {
        GameObject midCatcher = Instantiate(catcher);
        SetCatcher(midCatcher);
    }

    private void SetCatcher(GameObject catcher)
    {
        if (catcher != null)
        {
            CatcherControl catcherControl = catcher.GetComponent<CatcherControl>();
            if (catcherControl != null)
            {
                catcherControl.SetStartPos(transform.position);
            }
        }
    }
}
