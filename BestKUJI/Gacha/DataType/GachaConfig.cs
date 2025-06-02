using UnityEngine;
using System.Collections.Generic;

namespace GachaSystem.Data
{
    [CreateAssetMenu(fileName = "GachaConfig", menuName = "Gacha System/Config")]
    public class GachaConfig : ScriptableObject
    {
        public List<Prize> prizes = new List<Prize>();
    }
}