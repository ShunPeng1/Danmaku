using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    [DanmakuRoleClass]
    public class HeroineDanmakuRole : IDanmakuRole 
    {
        private DanmakuPlayerGroupModel _danmakuPlayerGroupModel;
        private DanmakuPlayerModel _myPlayerModel;

        public HeroineDanmakuRole()
        {
            _isRevealed = false;
        }

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

        DanmakuPlayerModel IDanmakuRole.MyPlayerModel
        {
            get => _myPlayerModel;
            set => _myPlayerModel = value;
        }

        public bool HasRole(DanmakuRoleEnum danmakuRoleEnum)
        {
            return DanmakuRoleEnum.Heroine == danmakuRoleEnum;
        }

        public bool IsGoalReached()
        {
            var bossPlayers = _danmakuPlayerGroupModel.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss) || player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

            return bossPlayers.All(bossPlayer => !bossPlayer.IsAlive);
        }

        public bool CanRevealRole()
        {
            return true; // TODO: Implement the reveal role logic
        }

        public IDanmakuRole RevealRole()
        {
            _isRevealed = true;
            return this;
        }
    }
    
    
}