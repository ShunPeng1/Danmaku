using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    [DanmakuRoleClass]
    public class RivalDanmakuRole : IDanmakuRole
    {
        private DanmakuPlayerGroupModel _danmakuPlayerGroupModel;
        private bool _isRevealed;
        private DanmakuPlayerModel _myPlayerModel;

        public RivalDanmakuRole()
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
                return danmakuRoleEnum is DanmakuRoleEnum.Heroine; 
            }
            return danmakuRoleEnum is DanmakuRoleEnum.Rival ;
        }

        public bool IsGoalReached()
        {
            var players = _danmakuPlayerGroupModel.Players.FindAll(
                player => player.Role.HasRole(DanmakuRoleEnum.Heroine)  
                          || player.Role.HasRole(DanmakuRoleEnum.StageBoss) 
                          || player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

            return players.All(player => !player.IsAlive);
        }

        public bool CanRevealRole()
        {
            return false; // TODO: Reveal RoleName when heroines are dead
        }

        public IDanmakuRole RevealRole()
        {
            _isRevealed = true;
            return this;
        }
    }
}