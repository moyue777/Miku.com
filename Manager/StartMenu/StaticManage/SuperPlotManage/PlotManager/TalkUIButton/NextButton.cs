using UnityEngine;
public class NextButton : MonoBehaviour
{
    public PlotManager plotManager;

    void Start()
    {
        plotManager = FindObjectOfType<PlotManager>();
    }
    public void Onclick()
    {
        plotManager.ClickNext();
    }
}