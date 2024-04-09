using System;
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockSetupPlayerView : DanmakuSetupPlayerBaseView
    {
        public override void SetupPlayerRoleView(Dictionary<DanmakuPlayerModel, IDanmakuRole> playerToRole)
        {
            foreach (var (player, role) in playerToRole)
            {
                Debug.Log($"Player {player.PlayerId} has role {role.GetType()}");
            }
        }

        public override void SetupCardDeckRoleView(DanmakuCardDeckModel cardDeckModel)
        {
            
        }

        public override void SetupPlayerHandView(DanmakuPlayerModel playerModel, DanmakuCardHandModel cardHandModel)
        {
            
        }
    }
}