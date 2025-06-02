using UnityEngine;

namespace GachaSystem.Data
{
    [System.Serializable]
    public class Prize
    {
        public string id;
        public string displayName;
        public Sprite icon;
        public int maxCount;
        [Range(0, 1)] public float baseProbability;
        [HideInInspector] public int remaining;
    }
}