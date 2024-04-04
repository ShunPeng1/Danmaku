using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class HeroineDanmakuRole : IDanmakuRole 
    {
        private DanmakuPlayerSubsystem _danmakuPlayerSubsystem;

        private bool IsRevealed { get; set;}

        bool IDanmakuRole.IsRevealed
        {
            get => IsRevealed;
            set => IsRevealed = value;
        }

        DanmakuPlayerSubsystem IDanmakuRole.DanmakuPlayerSubsystem
        {
            get => _danmakuPlayerSubsystem;
            set => _danmakuPlayerSubsystem = value;
        }
        
        public bool HasRole(DanmakuRoleEnum danmakuRoleEnum)
        {
            return DanmakuRoleEnum.Heroine == danmakuRoleEnum;
        }

        public int MinPlayerCountForAvailable() => 4;

        public bool IsGoalReached()
        {
            var bossPlayers = _danmakuPlayerSubsystem.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss) || player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

            return bossPlayers.All(bossPlayer => !bossPlayer.IsAlive);
        }

        public bool CanRevealRole()
        {
            return true; // TODO: Implement the reveal role logic
        }

        public void RevealRole()
        {
            IsRevealed = true;
        }
    }
    
    
}