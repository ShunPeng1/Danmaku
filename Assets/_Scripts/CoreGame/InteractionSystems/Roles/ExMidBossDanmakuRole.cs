using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    public class ExMidBossDanmakuRole : IDanmakuRole
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
                return danmakuRoleEnum is DanmakuRoleEnum.ExtraBoss or DanmakuRoleEnum.Partner;
            }
            return danmakuRoleEnum is DanmakuRoleEnum.Partner;
        }

        public bool IsGoalReached()
        {
            var heroinePlayers = _danmakuPlayerController.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.Heroine));
            var stageBossPlayers  = _danmakuPlayerController.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss));

            return heroinePlayers.All(heroinePlayer => !heroinePlayer.IsAlive) && stageBossPlayers.All(stageBossPlayer => !stageBossPlayer.IsAlive);
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