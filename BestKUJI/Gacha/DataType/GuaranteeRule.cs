namespace GachaSystem.Data
{
    [System.Serializable]
    public class GuaranteeRule
    {
        public bool enabled = true;
        public int triggerThreshold = 50;
        public float rateMultiplier = 2f;
        public string targetPrizeID = "SSR";
    }
}