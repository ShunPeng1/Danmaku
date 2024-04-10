using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuBoardController
    {
        private DanmakuInteractionController _danmakuInteractionController;
        private DanmakuPlayerGroupModel PlayerGroupModel => _danmakuInteractionController.PlayerGroupModel;
        private DanmakuBoardModel BoardModel => _danmakuInteractionController.BoardModel;
        
        private DanmakuSetupPlayerBaseView SetupPlayerView => _danmakuInteractionController.InteractionViewRepo.SetupPlayerView;
        
        public DanmakuBoardController(DanmakuInteractionController danmakuInteractionController)
        {
            _danmakuInteractionController = danmakuInteractionController;
        }
        
        
        public void StartupDraw()
        {
            foreach (var player in PlayerGroupModel.Players)
            {
                for (int i = 0; i < player.HandSize.Get(); i++)
                {
                    DrawCard(player);
                }
                
            }
        }


        public void DrawCard(DanmakuPlayerModel player)
        {
            var card = BoardModel.MainDeckModel.PopCardFront();
            player.CardHandModel.AddCard(card);
            
            var playerHandView = SetupPlayerView.GetPlayerView(player).CardHandView;
            playerHandView.DrawCard(card);
        }
        
        public void DrawCards(DanmakuPlayerModel player, int count)
        {
            var cards = new List<IDanmakuCard>();

            for (int i = 0; i < count; i++)
            {
                var card = BoardModel.MainDeckModel.PopCardFront();
                cards.Add(card);
                player.CardHandModel.AddCard(card);
            }
            
            var playerHandView = SetupPlayerView.GetPlayerView(player).CardHandView;
            playerHandView.DrawCards(cards);
        }
        
    }
}