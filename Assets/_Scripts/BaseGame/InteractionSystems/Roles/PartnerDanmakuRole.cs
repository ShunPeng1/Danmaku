﻿using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    [DanmakuRoleClass]
    public class PartnerDanmakuRole : IDanmakuRole
    {
        private DanmakuPlayerGroupModel _danmakuPlayerGroupModel;
        private bool _isRevealed;
        private DanmakuPlayerModel _myPlayerModel;

        public PartnerDanmakuRole()
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
            return DanmakuRoleEnum.Partner == danmakuRoleEnum;
        }

        public bool IsGoalReached()
        {
            var bossPlayers = _danmakuPlayerGroupModel.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss) || player.Role.HasRole(DanmakuRoleEnum.ExtraBoss));

            return bossPlayers.All(bossPlayer => !bossPlayer.IsAlive);
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