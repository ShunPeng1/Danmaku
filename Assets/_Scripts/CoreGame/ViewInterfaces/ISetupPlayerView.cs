using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems.Roles;

namespace _Scripts.CoreGame.InteractionSystems.Interfaces
{
    public interface ISetupPlayerView
    {
        public void SetupPlayerRoleView(Dictionary<DanmakuPlayer,IDanmakuRole> playerToRoles);
        
        
    }
}