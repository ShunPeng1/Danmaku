using UnityEngine;

namespace _Scripts.CoreGame.Configurations
{
    [CreateAssetMenu(fileName = "StartupStatsConfig", menuName = "Configurations/StartupStatsConfig")]
    public class StartupStatsConfig : ScriptableObject
    {
        public int StartingHealth;
        public int MaxHealth;
        
        public int StartingHandSize;
        public int MaxHandSize;

        public int StartingRange;
        public int MinRange;
        
        public int StartingDistant;
        public int MinDistant;
        
        public int StartingPower;
        public int MinPower;
        public int MaxPower;

    }
}