using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class ExBossDanmakuRole : IDanmakuRole
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
            return DanmakuRoleEnum.ExtraBoss == danmakuRoleEnum;
        }
        

        public bool IsGoalReached()
        {
            var stageBossOrHeroinePlayers = _danmakuPlayerController.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss) || player.Role.HasRole(DanmakuRoleEnum.Heroine));

            return stageBossOrHeroinePlayers.All(bossPlayer => !bossPlayer.IsAlive);
        }

        public bool CanRevealRole()
        {
            return false;
        }

        public void RevealRole()
        {
            _isRevealed = true;
        }
    }
}