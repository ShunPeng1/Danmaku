using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    [DanmakuRoleClass]
    public class ExMidBossDanmakuRole : IDanmakuRole
    {
        private DanmakuPlayerGroupModel _danmakuPlayerGroupModel;
        private bool _isRevealed;
        private DanmakuPlayerModel _myPlayerModel;

        public ExMidBossDanmakuRole()
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
            if (_isRevealed)
            {
                return danmakuRoleEnum is DanmakuRoleEnum.ExtraBoss or DanmakuRoleEnum.Partner;
            }
            return danmakuRoleEnum is DanmakuRoleEnum.Partner;
        }

        public bool IsGoalReached()
        {
            var heroinePlayers = _danmakuPlayerGroupModel.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.Heroine));
            var stageBossPlayers  = _danmakuPlayerGroupModel.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss));

            return heroinePlayers.All(heroinePlayer => !heroinePlayer.IsAlive) && stageBossPlayers.All(stageBossPlayer => !stageBossPlayer.IsAlive);
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