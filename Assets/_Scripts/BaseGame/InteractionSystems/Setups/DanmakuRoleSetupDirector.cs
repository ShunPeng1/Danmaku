using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using Shun_Utilities;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.Setups
{
    
    public class DanmakuRoleSetupDirector
    {
        private DanmakuPlayerGroupModel _danmakuPlayerGroupModel;
        private List<DanmakuPlayerModel> _players;
        private RoleSetConfig _roleSetConfig;
        
        public DanmakuRoleSetupDirector(DanmakuPlayerGroupModel danmakuPlayerGroupModel, List<DanmakuPlayerModel> players, RoleSetConfig roleSetConfig)
        {
            _danmakuPlayerGroupModel = danmakuPlayerGroupModel;
            _players = players;
            _roleSetConfig = roleSetConfig;
        }
        
        public Dictionary<DanmakuPlayerModel ,IDanmakuRole> SetupRoles()
        {
            try
            {
                RoleDistributionConfig roleDistribution = _roleSetConfig.RoleDistributions.FirstOrDefault(x => x.PlayerCount == _players.Count);
                
                List<IDanmakuRole> roles = GetRoles(roleDistribution);
                
                roles.Shuffle();
                
                Dictionary<DanmakuPlayerModel, IDanmakuRole> playerToRoles = new ();
                
                for (int i = 0; i < roles.Count; i++)
                {
                    roles[i].DanmakuPlayerGroupModel = _danmakuPlayerGroupModel;
                    roles[i].MyPlayerModel = _players[i];
                    _players[i].InitializeRole(roles[i]);
                    playerToRoles.Add(_players[i], roles[i]);
                }
                
                return playerToRoles;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }

        private List<IDanmakuRole> GetRoles(RoleDistributionConfig roleDistributionConfig)
        {
            List<IDanmakuRole> roles = new List<IDanmakuRole>();

            var heroineRoles = roleDistributionConfig.GetRoleFromEnum(DanmakuRoleEnum.Heroine);
            var stageBossRoles = roleDistributionConfig.GetRoleFromEnum(DanmakuRoleEnum.StageBoss);
            var partnerRoles = roleDistributionConfig.GetRoleFromEnum(DanmakuRoleEnum.Partner);
            var extraBossRoles = roleDistributionConfig.GetRoleFromEnum(DanmakuRoleEnum.ExtraBoss);
            var rivalRoles = roleDistributionConfig.GetRoleFromEnum(DanmakuRoleEnum.Rival);
            
            
            List<string> roleNames = new List<string>();

            if (heroineRoles.Count > 0)
            {
                RandomBag<string> heroinesBag = new (heroineRoles.ToArray(),1);
                for (int i = 0; i < roleDistributionConfig.HeroineRoleCount; i++)
                {
                    roleNames.Add(heroinesBag.PopRandomItem());
                }
            }
            
            if (stageBossRoles.Count > 0)
            {
                RandomBag<string> stageBossBag = new (stageBossRoles.ToArray(),1);
                for (int i = 0; i < roleDistributionConfig.StageBossRoleCount; i++)
                {
                    roleNames.Add(stageBossBag.PopRandomItem());
                }
            }
            
            if (partnerRoles.Count > 0)
            {
                RandomBag<string> partnerBag = new (partnerRoles.ToArray(),1);
                for (int i = 0; i < roleDistributionConfig.PartnerRoleCount; i++)
                {
                    roleNames.Add(partnerBag.PopRandomItem());
                }
            }
            
            if (extraBossRoles.Count > 0)
            {
                RandomBag<string> extraBossBag = new (extraBossRoles.ToArray(),1);
                for (int i = 0; i < roleDistributionConfig.ExtraBossRoleCount; i++)
                {
                    roleNames.Add(extraBossBag.PopRandomItem());
                }
            }
            
            if (rivalRoles.Count > 0)
            {
                RandomBag<string> rivalBag = new (rivalRoles.ToArray(),1);
                for (int i = 0; i < roleDistributionConfig.RivalRoleCount; i++)
                {
                    roleNames.Add(rivalBag.PopRandomItem());
                }
            }

            var roleFactory = new DanmakuRoleFactory();
            foreach (var roleName in roleNames)
            {
                var role = roleFactory.CreateRole(roleName);
                roles.Add(role);
            }
            
            return roles;
        } 
    }
}