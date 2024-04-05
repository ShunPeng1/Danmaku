using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    [DanmakuRoleClass]
    public class OneTruePartnerDanmakuRole : IDanmakuRole
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
            return danmakuRoleEnum is DanmakuRoleEnum.Partner;
        }

        public bool IsGoalReached()
        {
            var players = _danmakuPlayerController.Players.FindAll(
                player => player.Role.HasRole(DanmakuRoleEnum.Partner) 
                          || player.Role.HasRole(DanmakuRoleEnum.StageBoss) 
                          || player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

            return players.All(player => !player.IsAlive);
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