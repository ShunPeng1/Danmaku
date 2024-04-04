using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class FinalBossDanmakuRole : IDanmakuRole
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
            return DanmakuRoleEnum.StageBoss == danmakuRoleEnum;
        }

        public int MinPlayerCountForAvailable() => 5;

        public bool IsGoalReached()
        {
            var heroinePlayers = _danmakuPlayerSubsystem.PlayerRoles.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.Heroine));
            var partnerPlayers = _danmakuPlayerSubsystem.PlayerRoles.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.Partner));

            return heroinePlayers.All(heroinePlayer => !heroinePlayer.IsAlive) && partnerPlayers.Exists(partnerPlayer => !partnerPlayer.IsAlive);
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