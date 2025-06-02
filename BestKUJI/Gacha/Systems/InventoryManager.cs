using System.Collections.Generic;
using GachaSystem.Data;

namespace GachaSystem.Systems
{
    public class InventoryManager
    {
        private readonly Dictionary<string, Prize> _prizeMap = new Dictionary<string, Prize>();

        public InventoryManager(GachaConfig config)
        {
            foreach (var prize in config.prizes)
            {
                prize.remaining = prize.maxCount;
                _prizeMap[prize.id] = prize;
            }
        }

        public bool IsAvailable(string prizeID)
        {
            return _prizeMap.ContainsKey(prizeID) && 
                   _prizeMap[prizeID].remaining > 0;
        }

        public void UpdateInventory(string prizeID)
        {
            if (_prizeMap.ContainsKey(prizeID))
            {
                _prizeMap[prizeID].remaining--;
            }
        }
    }
}