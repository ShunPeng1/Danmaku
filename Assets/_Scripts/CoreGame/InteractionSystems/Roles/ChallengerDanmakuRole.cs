using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class ChallengerDanmakuRole : IDanmakuRole
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


        public bool IsGoalReached()
        {
            var heroinePlayers = _danmakuPlayerSubsystem.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.Heroine));
            var extraBossPlayers  = _danmakuPlayerSubsystem.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

            return heroinePlayers.All(heroinePlayer => !heroinePlayer.IsAlive) && extraBossPlayers.All(extraBossPlayer => !extraBossPlayer.IsAlive);
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