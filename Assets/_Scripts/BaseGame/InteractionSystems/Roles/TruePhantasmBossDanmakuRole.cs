﻿using System.Linq;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Roles
{
    [DanmakuRoleClass]
    public class TruePhantasmBossDanmakuRole : IDanmakuRole
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
            return DanmakuRoleEnum.ExtraBoss == danmakuRoleEnum;
        }

        public bool IsGoalReached()
        {
            var stageBossOrHeroinePlayers = _danmakuPlayerGroupModel.Players.FindAll(player => player.Role.HasRole(DanmakuRoleEnum.StageBoss) || player.Role.HasRole(DanmakuRoleEnum.Heroine));

            return stageBossOrHeroinePlayers.All(bossPlayer => !bossPlayer.IsAlive);
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