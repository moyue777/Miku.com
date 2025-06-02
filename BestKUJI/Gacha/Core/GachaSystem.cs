using GachaSystem.Data;
using GachaSystem.Systems;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace GachaSystem.Core
{
    public class GachaSystem : MonoBehaviour
    {
        [SerializeField] private GachaConfig config;
        private ProbabilityCalculator _calculator;
        private InventoryManager _inventory;

        private void Awake()
        {
            _inventory = new InventoryManager(config);
            _calculator = new ProbabilityCalculator(config, _inventory);
        }

        public Prize DrawSingle()
        {
            var adjustedRates = _calculator.GetDynamicProbabilities();
            var prizeID = CalculateResult(adjustedRates);
            _inventory.UpdateInventory(prizeID);

            return config.prizes.Find(p => p.id == prizeID);
        }

        private string CalculateResult(Dictionary<string, float> rates)
        {
            if (rates == null || rates.Count == 0)
                throw new System.ArgumentException("Rates dictionary is null or empty.");

            float totalRate = 0f;
            foreach (var rate in rates.Values)
            {
                totalRate += rate;
            }

            float randomValue = Random.Range(0f, totalRate);
            foreach (var kvp in rates)
            {
                randomValue -= kvp.Value;
                if (randomValue <= 0f)
                {
                    return kvp.Key;
                }
            }

            // 理论上不会走到这里，但防止浮点精度问题
            return rates.Keys.FirstOrDefault();
        }
    }
}