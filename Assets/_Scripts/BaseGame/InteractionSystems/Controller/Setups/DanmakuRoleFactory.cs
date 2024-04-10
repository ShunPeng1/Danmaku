using System;
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.Setups
{
    public class DanmakuRoleFactory
    {
        private readonly Dictionary<string, Func<IDanmakuRole>> _roleFactories = new()
        {
            { "HeroineDanmakuRole", () => new HeroineDanmakuRole() },
            { "StageBossDanmakuRole", () => new StageBossDanmakuRole() },
            { "PartnerDanmakuRole", () => new PartnerDanmakuRole() },
            { "ExBossDanmakuRole", () => new ExBossDanmakuRole() },
            { "PhantasmBossDanmakuRole", () => new PhantasmBossDanmakuRole() },
            { "FinalBossDanmakuRole", () => new FinalBossDanmakuRole() },
            { "ChallengerDanmakuRole", () => new ChallengerDanmakuRole() },
            { "AntiHeroineDanmakuRole", () => new AntiHeroineDanmakuRole() },
            { "ExMidBossDanmakuRole", () => new ExMidBossDanmakuRole() },
            { "OneTruePartnerDanmakuRole", () => new OneTruePartnerDanmakuRole() },
            { "RivalDanmakuRole", () => new RivalDanmakuRole() }
        };

        public IDanmakuRole CreateRole(string roleName)
        {
            Debug.Log("Creating role: " + roleName);

            if (_roleFactories.TryGetValue(roleName, out var roleFactory))
            {
                return roleFactory();
            }
            else
            {
                Debug.LogError("Role not found: " + roleName);
                return null;
            }
        }

        public IDanmakuRole CreateRoleByType(string roleName)
        {
            var roleType = Type.GetType(roleName);

            if (roleType != null) return (IDanmakuRole)Activator.CreateInstance(roleType);
            else
            {
                Debug.LogError("Role not found: " + roleName);
                return null;
            }
        }
        
    }
}