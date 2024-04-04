using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.CoreGame.Configurations
{
    [CreateAssetMenu(fileName = "RoleSetConfig", menuName = "Configurations/RoleSetConfig")]
    public class RoleSetConfig : ScriptableObject
    {
        public List<RoleDistributionConfig> RoleDistributions;
        
        private void OnValidate()
        {
            Dictionary<int, RoleDistributionConfig> roleDistributionDict = new ();
            foreach (var roleDistribution in RoleDistributions)
            {   
                if (roleDistributionDict.ContainsKey(roleDistribution.PlayerCount))
                {
                    throw new System.ArgumentException("RoleDistributionConfig with the same PlayerCount already exists");
                }
                roleDistributionDict.Add(roleDistribution.PlayerCount, roleDistribution);
            }
            
        }
        
    }
    
}