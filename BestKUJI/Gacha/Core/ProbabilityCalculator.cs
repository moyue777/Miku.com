using System.Collections.Generic;
using GachaSystem.Data;
using GachaSystem.Systems;

namespace GachaSystem.Core
{
    public class ProbabilityCalculator
    {
        private readonly GachaConfig _config;
        private readonly InventoryManager _inventory;

        public ProbabilityCalculator(GachaConfig config, InventoryManager inventory)
        {
            _config = config;
            _inventory = inventory;
        }

        public Dictionary<string, float> GetDynamicProbabilities()
        {
            var probabilities = new Dictionary<string, float>();
            float totalWeight = 0f;

            foreach (var prize in _config.prizes)
            {
                if (_inventory.IsAvailable(prize.id))
                {
                    totalWeight += prize.baseProbability;
                }
            }

            foreach (var prize in _config.prizes)
            {
                if (_inventory.IsAvailable(prize.id))
                {
                    probabilities[prize.id] = prize.baseProbability / totalWeight;
                }
            }

            return probabilities;
        }
    }
}