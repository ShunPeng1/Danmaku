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

        public override void SetupCardDeck(DanmakuCardDeckModel mainDeckModel, DanmakuCardDeckModel discardDeckModel,
            DanmakuCardDeckModel incidentDeckModel)
        {
            Debug.Log("Setting up card decks");
            foreach (var danmakuCard in mainDeckModel.Cards)
            {
                Debug.Log("Main deck card: " + danmakuCard.GetType());
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