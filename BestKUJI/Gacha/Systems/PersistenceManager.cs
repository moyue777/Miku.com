using UnityEngine;
using GachaSystem.Data;

namespace GachaSystem.Systems
{
    public class PersistenceManager
    {
        private const string SAVE_KEY = "GachaData_";
        
        public void SaveData(GachaConfig config)
        {
            foreach (var prize in config.prizes)
            {
                PlayerPrefs.SetInt(SAVE_KEY + prize.id, prize.remaining);
            }
        }

        public void LoadData(GachaConfig config)
        {
            foreach (var prize in config.prizes)
            {
                prize.remaining = PlayerPrefs.GetInt(SAVE_KEY + prize.id, prize.maxCount);
            }
        }
    }
}