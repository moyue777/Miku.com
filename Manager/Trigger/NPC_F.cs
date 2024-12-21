using UnityEngine;
public class NPC_F : MyTrigger
{
    public PlotManager plotManager;
    public int NPC_id;
    void Start()
    {
        plotManager = FindObjectOfType<PlotManager>();
    }
    protected override void OnFKeyPressed()
    {
        plotManager.TriggerSend(NPC_id);
    }
}