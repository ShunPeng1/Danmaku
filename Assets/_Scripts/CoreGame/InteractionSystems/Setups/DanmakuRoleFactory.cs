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
                case "DanmakuRole":
                    return new HeroineDanmakuRole();
                case "DanmakuRole2":
                    return new StageBossDanmakuRole();
                case "DanmakuRole3":
                    return new PartnerDanmakuRole();
                case "DanmakuRole4":
                    return new ExBossDanmakuRole();
                case "DanmakuRole5":
                    return new PhantasmBossDanmakuRole();
                case "DanmakuRole6":
                    return new FinalBossDanmakuRole();
                case "DanmakuRole7":
                    return new ChallengerDanmakuRole();
                case "DanmakuRole8":
                    return new AntiHeroineDanmakuRole();
                case "DanmakuRole9":
                    return new ExMidBossDanmakuRole();
                case "DanmakuRole10":
                    return new OneTruePartnerDanmakuRole();
                case "DanmakuRole11":
                    return new RivalDanmakuRole();
                default:
                    Debug.LogError("Role not found: " + roleName);
                    return null;
            }
            
            
        }
        
    }
}