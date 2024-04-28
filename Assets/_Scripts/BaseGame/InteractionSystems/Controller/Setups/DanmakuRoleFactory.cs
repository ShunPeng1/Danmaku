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
            { nameof(HeroineDanmakuRole), () => new HeroineDanmakuRole() },
            { nameof(StageBossDanmakuRole), () => new StageBossDanmakuRole() },
            { nameof(PartnerDanmakuRole), () => new PartnerDanmakuRole() },
            { nameof(ExBossDanmakuRole), () => new ExBossDanmakuRole() },
            { nameof(PhantasmBossDanmakuRole), () => new PhantasmBossDanmakuRole() },
            { nameof(FinalBossDanmakuRole), () => new FinalBossDanmakuRole() },
            { nameof(ChallengerDanmakuRole), () => new ChallengerDanmakuRole() },
            { nameof(AntiHeroineDanmakuRole), () => new AntiHeroineDanmakuRole() },
            { nameof(ExMidBossDanmakuRole), () => new ExMidBossDanmakuRole() },
            { nameof(OneTruePartnerDanmakuRole), () => new OneTruePartnerDanmakuRole() },
            { nameof(RivalDanmakuRole), () => new RivalDanmakuRole() }
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