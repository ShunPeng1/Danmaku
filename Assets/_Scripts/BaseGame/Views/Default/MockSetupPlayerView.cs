using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockSetupPlayerView : DanmakuSetupPlayerBaseView
    {
        public override IEnumerator SetupPlayerRoleView(Dictionary<DanmakuPlayerModel, IDanmakuRole> playerToRole)
        {
            foreach (var (player, role) in playerToRole)
            {
                Debug.Log($"Player {player.PlayerId} has role {role.GetType()}");
            }
            
            yield break;
        }

        public override IEnumerator SetupCardDeckRoleView(DanmakuCardDeckModel cardDeckModel)
        {
            
            yield break;
        }

        public override IEnumerator SetupPlayerHandView(DanmakuPlayerModel playerModel, DanmakuCardHandModel cardHandModel)
        {
            yield break;
        }
    }
}