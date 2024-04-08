using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    [DanmakuRoleClass]
    public class OneTruePartnerDanmakuRole : IDanmakuRole
    {
        private DanmakuPlayerGroupModel _danmakuPlayerGroupModel;
        private bool _isRevealed;
        private DanmakuPlayerModel _myPlayerModel;

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
            return danmakuRoleEnum is DanmakuRoleEnum.Partner;
        }

        public bool IsGoalReached()
        {
            var players = _danmakuPlayerGroupModel.Players.FindAll(
                player => player.Role.HasRole(DanmakuRoleEnum.Partner) 
                          || player.Role.HasRole(DanmakuRoleEnum.StageBoss) 
                          || player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

            return players.All(player => !player.IsAlive);
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