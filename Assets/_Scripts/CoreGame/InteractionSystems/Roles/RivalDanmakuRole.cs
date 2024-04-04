using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class RivalDanmakuRole : IDanmakuRole
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
            if (_isRevealed)
            {
                return danmakuRoleEnum is DanmakuRoleEnum.Heroine; 
            }
            return danmakuRoleEnum is DanmakuRoleEnum.Rival ;
        }

        public int MinPlayerCountForAvailable() => 8;

        public bool IsGoalReached()
        {
            var players = _danmakuPlayerSubsystem.Players.FindAll(
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