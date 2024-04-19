using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockBoardView : DanmakuBoardBaseView
    {
        protected override void InitializeInherit()
        {
            
        }

        public override Dictionary<DanmakuPlayerModel, DanmakuPlayerBaseView> CreatePlayerViews(List<DanmakuPlayerModel> playerModels)
        {
            var playerModelToViews = new Dictionary<DanmakuPlayerModel, DanmakuPlayerBaseView>();
            foreach (var playerModel in playerModels)
            {
                var playerView = new GameObject("Mock Player View").AddComponent<MockPlayerView>();
                playerView.name = $"PlayerView_{playerModel.PlayerId}";
                playerModelToViews.Add(playerModel, playerView);
                
                playerView.InitializeView();
            }
            
            return playerModelToViews;
        }

        public override void DrawCardFromMainDeck(DanmakuPlayerModel playerModel, DanmakuMainDeckCardModel card)
        {
            var handView = InteractionViewRepo.GetPlayerView(playerModel).CardHandView;
            DanmakuMainDeckCardBaseView cardView = new GameObject("Mock Card ").AddComponent<DanmakuMainDeckCardBaseView>();
            handView.AddCard(cardView,card);
        }

        public override void DrawCardFromMainDeck(DanmakuPlayerModel playerModel, List<DanmakuMainDeckCardModel> cards)
        {
            foreach (var card in cards)
            {
                DrawCardFromMainDeck(playerModel, card);
            }
        }

        public override void DiscardCardToDiscardDeck(DanmakuPlayerModel playerModel, DanmakuMainDeckCardModel card)
        {
            
        }

        public override void DiscardCardsToDiscardDeck(DanmakuPlayerModel playerModel, List<DanmakuMainDeckCardModel> cards)
        {
            
        }

        public override void SetupMainDeck(DanmakuCardDeckModel mainDeckModel, DanmakuCardDeckModel discardDeckModel)
        {
            
        }

        public override void SetupIncidentDeck(DanmakuCardDeckModel incidentDeckModel)
        {
            
        }
    }
}