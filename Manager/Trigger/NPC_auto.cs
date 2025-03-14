public class NPC_auto : MyTrigger
{
    public PlotManager plotManager;
    public SuperController superController;
    public string NPC_id;
    private bool hasTriggered = false;
    void Start()
    {
        superController = SuperController.Instance;
        plotManager = superController.plotManager;
    }
    protected override void OnFKeyPressed()
    {
        if (superController.isTalking == false)
        {
            plotManager.TriggerSend(NPC_id);
        }
    }
    override public void Update()
    {
        if (!hasTriggered && touched && playerRigidbody != null && playerRigidbody.velocity.sqrMagnitude < 0.01f)
        {
            OnFKeyPressed();
            hasTriggered = true;
        }
    }
}
