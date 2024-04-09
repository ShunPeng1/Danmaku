using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    [DanmakuRoleClass]
    public class ChallengerDanmakuRole : IDanmakuRole
    {
        private DanmakuPlayerGroupModel _danmakuPlayerGroupModel;
        private bool _isRevealed;
        private DanmakuPlayerModel _myPlayerModel;

        public ChallengerDanmakuRole()
        {
            _isRevealed = false;
        }
        
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

        DanmakuPlayerModel IDanmakuRole.MyPlayerModel
        {
            get => _myPlayerModel;
            set => _myPlayerModel = value;
        }

        public bool HasRole(DanmakuRoleEnum danmakuRoleEnum)
        {
            return DanmakuRoleEnum.StageBoss == danmakuRoleEnum;
        }


        public bool IsGoalReached()
        {
            var heroinePlayers = _danmakuPlayerGroupModel.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.Heroine));
            var extraBossPlayers  = _danmakuPlayerGroupModel.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

            return heroinePlayers.All(heroinePlayer => !heroinePlayer.IsAlive) && extraBossPlayers.All(extraBossPlayer => !extraBossPlayer.IsAlive);
        }

        public bool CanRevealRole()
        {
            return false;
        }

        public IDanmakuRole RevealRole()
        {
            _isRevealed = true;
            return this;
        }
    }
}