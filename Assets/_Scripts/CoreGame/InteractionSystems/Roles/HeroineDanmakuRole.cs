using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class HeroineDanmakuRole : IDanmakuRole 
    {
        private DanmakuPlayerController _danmakuPlayerController;

        private bool IsRevealed { get; set;}

        bool IDanmakuRole.IsRevealed
        {
            get => IsRevealed;
            set => IsRevealed = value;
        }

        DanmakuPlayerController IDanmakuRole.DanmakuPlayerController
        {
            get => _danmakuPlayerController;
            set => _danmakuPlayerController = value;
        }
        
        public bool HasRole(DanmakuRoleEnum danmakuRoleEnum)
        {
            return DanmakuRoleEnum.Heroine == danmakuRoleEnum;
        }

        public bool IsGoalReached()
        {
            var bossPlayers = _danmakuPlayerController.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss) || player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

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