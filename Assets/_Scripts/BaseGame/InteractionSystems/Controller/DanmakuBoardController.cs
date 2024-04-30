using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuBoardController
    {
        private readonly DanmakuInteractionController _danmakuInteractionController;
        
        // Views
        private DanmakuInteractionViewRepo InteractionViewRepo => _danmakuInteractionController.InteractionViewRepo;
        
        // Models
        private DanmakuPlayerGroupModel PlayerGroupModel => _danmakuInteractionController.PlayerGroupModel;
        private DanmakuBoardModel BoardModel => _danmakuInteractionController.BoardModel;
        
        
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
            
            var boardView = _danmakuInteractionController.InteractionViewRepo.BoardView;
            boardView.DrawCardFromMainDeck(player, (DanmakuMainDeckCardModel) card);
            
            //var playerHandView = SetupPlayerView.GetPlayerView(player).CardHandView;
            //playerHandView.AddCard(card);
        }
        
        public void DrawCards(DanmakuPlayerModel player, int count)
        {
            var cards = new List<DanmakuMainDeckCardModel>();

            for (int i = 0; i < count; i++)
            {
                var card = BoardModel.MainDeckModel.PopCardFront();
                cards.Add((DanmakuMainDeckCardModel) card);
                player.CardHandModel.AddCard(card);
            }
            
            var boardView = _danmakuInteractionController.InteractionViewRepo.BoardView;
            boardView.DrawCardFromMainDeck(player, cards);

        }

        public void StartDrawCharacter(int eachPlayerCharacterChoiceCount)
        {
            foreach (var player in PlayerGroupModel.Players)
            {
                List<DanmakuCharacterCardModel> characterCards = new List<DanmakuCharacterCardModel>();
                for (int i = 0; i < eachPlayerCharacterChoiceCount; i++)
                {
                    characterCards.Add((DanmakuCharacterCardModel) BoardModel.CharacterDeckModel.PopCardFront());
                }
                
                InteractionViewRepo.BoardView.DrawCharacterCardsForSelection(player, characterCards);
            }
        }
    }
}