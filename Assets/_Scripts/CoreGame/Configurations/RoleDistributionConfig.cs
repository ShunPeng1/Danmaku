using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.CoreGame.Configurations
{
    [CreateAssetMenu(fileName = "RoleDistributionConfig", menuName = "Configurations/RoleDistributionConfig")]
    public class RoleDistributionConfig : ScriptableObject
    {
        public int PlayerCount = 4;
        public int HeroineRoleCount = 1;
        public int StageBossRoleCount = 2;
        public int ExtraBossRoleCount = 1;
        public int RivalRoleCount = 0;
        public int PartnerRoleCount = 0;

        public List<RoleScriptableData> RolesPool; 
        
        private void OnValidate()
        {
            if (PlayerCount < 1)
            {
                Debug.LogError("PlayerCount must be greater than 0");
            }

            if (HeroineRoleCount < 0)
            {
                Debug.LogError("HeroineRoleCount must be greater than or equal to 0");
            }

            if (StageBossRoleCount < 0)
            {
                Debug.LogError("StageBossRoleCount must be greater than or equal to 0");
            }

            if (ExtraBossRoleCount < 0)
            {
                Debug.LogError("ExtraBossRoleCount must be greater than or equal to 0");
            }

            if (RivalRoleCount < 0)
            {
                Debug.LogError("RivalRoleCount must be greater than or equal to 0");
            }

            if (PartnerRoleCount < 0)
            {
                Debug.LogError("PartnerRoleCount must be greater than or equal to 0");
            }

            StageBossRoleCount = PlayerCount - HeroineRoleCount - ExtraBossRoleCount - RivalRoleCount - PartnerRoleCount;
            
            if (RolesPool.Count < PlayerCount)
            {
                Debug.LogError("RolesPool count is less than PlayerCount");
            }

            Dictionary<DanmakuRoleEnum, int> roleCountDict = new();
            
            foreach (var roleConfig in RolesPool.Where(roleConfig => roleConfig.MinPlayerCountPrerequisite <= PlayerCount))
            {
                if (roleCountDict.ContainsKey(roleConfig.InitialRoleEnum))
                {
                    roleCountDict[roleConfig.InitialRoleEnum]++;
                }
                else
                {
                    roleCountDict[roleConfig.InitialRoleEnum] = 1;
                }
            }
            
            roleCountDict.TryGetValue(DanmakuRoleEnum.Heroine, out var heroineRoleCount);
            if (heroineRoleCount < HeroineRoleCount)
            {
                Debug.LogError("the number of Heroine roles in RolesPool must be greater than or equal to HeroineRoleCount");
            }
            
            roleCountDict.TryGetValue(DanmakuRoleEnum.StageBoss, out var stageBossRoleCount);
            if (stageBossRoleCount < StageBossRoleCount)
            {
                Debug.LogError("the number of StageBoss roles in RolesPool must be greater than or equal to StageBossRoleCount");
            }
            
            roleCountDict.TryGetValue(DanmakuRoleEnum.ExtraBoss, out var extraBossRoleCount);
            if (extraBossRoleCount < ExtraBossRoleCount)
            {
                Debug.LogError("the number of ExtraBoss roles in RolesPool must be greater than or equal to ExtraBossRoleCount");
            }

            roleCountDict.TryGetValue(DanmakuRoleEnum.Rival, out var rivalRoleCount);
            if (rivalRoleCount < RivalRoleCount)
            {
                Debug.LogError("the number of Rival roles in RolesPool must be greater than or equal to RivalRoleCount");
            }
            
            roleCountDict.TryGetValue(DanmakuRoleEnum.Partner, out var partnerRoleCount);
            if (partnerRoleCount < PartnerRoleCount)
            {
                Debug.LogError("the number of Partner roles in RolesPool must be greater than or equal to PartnerRoleCount");
            }
            
        }

        public List<string> GetRoleFromEnum(DanmakuRoleEnum danmakuRoleEnum)
        {
            return RolesPool.Where(roleConfig => roleConfig.InitialRoleEnum == danmakuRoleEnum)
                .Select(roleConfig => roleConfig.RoleName).ToList();
        }
        
    }
}