using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class TruePhantasmBossDanmakuRole : IDanmakuRole
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
            return DanmakuRoleEnum.ExtraBoss == danmakuRoleEnum;
        }

        public bool IsGoalReached()
        {
            var stageBossOrHeroinePlayers = _danmakuPlayerSubsystem.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss) || player.Role.HasRole(DanmakuRoleEnum.Heroine));

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