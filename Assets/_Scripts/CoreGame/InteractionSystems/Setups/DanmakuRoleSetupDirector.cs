using System;
using System.Collections.Generic;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using Shun_Utilities;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.Setups
{
    
    public class DanmakuRoleSetupDirector
    {
        private DanmakuPlayerController _danmakuPlayerController;
        private List<DanmakuPlayer.DanmakuPlayerBuilder> _players;
        private RoleSetConfig _roleSetConfig;
        
        public DanmakuRoleSetupDirector(DanmakuPlayerController danmakuPlayerController, List<DanmakuPlayer.DanmakuPlayerBuilder> players, RoleSetConfig roleSetConfig)
        {
            _danmakuPlayerController = danmakuPlayerController;
            _players = players;
            _roleSetConfig = roleSetConfig;
        }
        
        public Dictionary<DanmakuPlayer ,IDanmakuRole> SetupRoles()
        {
            try
            {
                List<IDanmakuRole> roles = GetRoles(_roleSetConfig.RoleDistributions[_players.Count]);
                
                roles.Shuffle();
                
                Dictionary<DanmakuPlayer, IDanmakuRole> playerToRoles = new ();
                
                for (int i = 0; i < roles.Count; i++)
                {
                    roles[i].DanmakuPlayerController = _danmakuPlayerController;
                    _players[i].WithDanmakuRole(roles[i]);
                    playerToRoles.Add(_players[i].Build(), roles[i]);
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
            
            RandomBag<string> heroinesBag = new (heroineRoles.ToArray(),1);
            RandomBag<string> stageBossBag = new (stageBossRoles.ToArray(),1);
            RandomBag<string> partnerBag = new (partnerRoles.ToArray(),1);
            RandomBag<string> extraBossBag = new (extraBossRoles.ToArray(),1);
            RandomBag<string> rivalBag = new (rivalRoles.ToArray(),1);
            
            List<string> roleNames = new List<string>();
            
            for (int i = 0; i < roleDistributionConfig.HeroineRoleCount; i++)
            {
                roleNames.Add(heroinesBag.PopRandomItem());
            }
            for (int i = 0; i < roleDistributionConfig.StageBossRoleCount; i++)
            {
                roleNames.Add(stageBossBag.PopRandomItem());
            }
            for (int i = 0; i < roleDistributionConfig.PartnerRoleCount; i++)
            {
                roleNames.Add(partnerBag.PopRandomItem());
            }
            for (int i = 0; i < roleDistributionConfig.ExtraBossRoleCount; i++)
            {
                roleNames.Add(extraBossBag.PopRandomItem());
            }
            for (int i = 0; i < roleDistributionConfig.RivalRoleCount; i++)
            {
                roleNames.Add(rivalBag.PopRandomItem());
            }


            foreach (var roleName in roleNames)
            {
                
            }
            
            return roles;
        } 
    }
}