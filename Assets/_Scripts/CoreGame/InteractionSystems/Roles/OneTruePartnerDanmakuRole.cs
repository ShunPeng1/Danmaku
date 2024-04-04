using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class OneTruePartnerDanmakuRole : IDanmakuRole
    {
        private DanmakuPlayerSubsystem _danmakuPlayerSubsystem;
        private bool _isRevealed;

        bool IDanmakuRole.IsRevealed
        {
            get => _isRevealed;
            set => _isRevealed = value;
        }

        DanmakuPlayerSubsystem IDanmakuRole.DanmakuPlayerSubsystem
        {
            get => _danmakuPlayerSubsystem;
            set => _danmakuPlayerSubsystem = value;
        }

        public bool HasRole(DanmakuRoleEnum danmakuRoleEnum)
        {
            return danmakuRoleEnum is DanmakuRoleEnum.Partner;
        }

        public int MinPlayerCountForAvailable() => 7;

        public bool IsGoalReached()
        {
            var players = _danmakuPlayerSubsystem.PlayerRoles.FindAll(
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