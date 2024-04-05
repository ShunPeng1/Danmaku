using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.Setups
{
    public class DanmakuRoleFactory
    {
        
        public IDanmakuRole CreateRole(string roleName)
        {
            Debug.Log("Creating role: " + roleName);

            switch (roleName)
            {
                case "HeroineDanmakuRole":
                    return new HeroineDanmakuRole();
                case "StageBossDanmakuRole":
                    return new StageBossDanmakuRole();
                case "PartnerDanmakuRole":
                    return new PartnerDanmakuRole();
                case "ExBossDanmakuRole":
                    return new ExBossDanmakuRole();
                case "PhantasmBossDanmakuRole":
                    return new PhantasmBossDanmakuRole();
                case "FinalBossDanmakuRole":
                    return new FinalBossDanmakuRole();
                case "ChallengerDanmakuRole":
                    return new ChallengerDanmakuRole();
                case "AntiHeroineDanmakuRole":
                    return new AntiHeroineDanmakuRole();
                case "ExMidBossDanmakuRole":
                    return new ExMidBossDanmakuRole();
                case "OneTruePartnerDanmakuRole":
                    return new OneTruePartnerDanmakuRole();
                case "RivalDanmakuRole":
                    return new RivalDanmakuRole();
                default:
                    Debug.LogError("Role not found: " + roleName);
                    return null;
            }
            
            
        }
        
    }
}