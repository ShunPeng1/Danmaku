using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    [DanmakuRoleClass]
    public class AntiHeroineDanmakuRole : IDanmakuRole
    {
        private DanmakuPlayerGroupModel _danmakuPlayerGroupModel;
        private bool _isRevealed;

        bool IDanmakuRole.IsRevealed
        {
            get => _isRevealed;
            set => _isRevealed = value;
        }

        DanmakuPlayerGroupModel IDanmakuRole.DanmakuPlayerGroupModel
        {
            get => _danmakuPlayerGroupModel;
            set => _danmakuPlayerGroupModel = value;
        }
        
        public bool HasRole(DanmakuRoleEnum danmakuRoleEnum)
        {
            return DanmakuRoleEnum.StageBoss == danmakuRoleEnum;
        }

        public bool IsGoalReached()
        {
            var heroinePlayers = _danmakuPlayerGroupModel.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.Heroine));
            var stageBossPlayers  = _danmakuPlayerGroupModel.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss));

            return heroinePlayers.All(heroinePlayer => !heroinePlayer.IsAlive) && stageBossPlayers.Exists(stageBossPlayer => !stageBossPlayer.IsAlive);
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