using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class RivalDanmakuRole : IDanmakuRole
    {
        private DanmakuPlayerController _danmakuPlayerController;
        private bool _isRevealed;

        bool IDanmakuRole.IsRevealed
        {
            get => _isRevealed;
            set => _isRevealed = value;
        }

        DanmakuPlayerController IDanmakuRole.DanmakuPlayerController
        {
            get => _danmakuPlayerController;
            set => _danmakuPlayerController = value;
        }

        public bool HasRole(DanmakuRoleEnum danmakuRoleEnum)
        {
            if (_isRevealed)
            {
                return danmakuRoleEnum is DanmakuRoleEnum.Heroine; 
            }
            return danmakuRoleEnum is DanmakuRoleEnum.Rival ;
        }

        public bool IsGoalReached()
        {
            var players = _danmakuPlayerController.Players.FindAll(
                player => player.Role.HasRole(DanmakuRoleEnum.Heroine)  
                          || player.Role.HasRole(DanmakuRoleEnum.StageBoss) 
                          || player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

            return players.All(player => !player.IsAlive);
        }

        public bool CanRevealRole()
        {
            return false; // TODO: Reveal Role when heroines are dead
        }

        public void RevealRole()
        {
            _isRevealed = true;
        }
    }
}