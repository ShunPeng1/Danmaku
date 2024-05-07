using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views;
using _Scripts.BaseGame.Views.Basics;
using Shun_Utilities;

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
            var menus = new ObservableList<DanmakuSessionMenu>();
            
            var session = new DanmakuSession.Builder()
                .WithPlayingPlayerModel(PlayerGroupModel.Players.ConvertAll(player => (IDanmakuActivator) player))
                .WithPlayerSessionKindEnum(EndSessionKindEnum.AllPlayed)
                .WithPlayingSessionMenus(menus)
                .WithCountDownTime(30f)
                .WithOnSessionEnd(AssignCharacterCard, true)
                
                .Build(_danmakuInteractionController);

            
            foreach (var player in PlayerGroupModel.Players)
            {
                List<DanmakuCharacterCardModel> characterCards = new List<DanmakuCharacterCardModel>();
                for (int i = 0; i < eachPlayerCharacterChoiceCount; i++)
                {
                    characterCards.Add((DanmakuCharacterCardModel) BoardModel.CharacterDeckModel.PopCardFront());
                }
                
                // Create a menu for each player
                var characterCardChoices = characterCards.ConvertAll(card => (card) as IDanmakuTargetable);
                var sessionChoices = new List<DanmakuSessionChoice>();
                
                DanmakuSessionMenu menu = new DanmakuSessionMenu(session, player, sessionChoices);
                
                sessionChoices.Add(new DanmakuSessionChoice(menu, characterCardChoices, ChoiceActionEnum.AutoCheck));
                
                menus.Add(menu);
                
                // Draw the character cards for selection
                InteractionViewRepo.BoardView.DrawCharacterCardsForSelection(player, characterCards);
                
            }
            
            // Remove the session from the player when the session ends
            session.SubscribeOnSessionEnd(InteractionViewRepo.BoardView.RemoveSessionFromPlayer);
            session.OnSessionStartEvent.Subscribe(InteractionViewRepo.BoardView.AddSessionToPlayer);
            
            
            // Start the session
            session.StartSession();
        }
        
        public void AssignCharacterCard(List<DanmakuSessionMenu> danmakuSessionMenus)
        {
            
            foreach (var menu in danmakuSessionMenus)
            {
                var player = (DanmakuPlayerModel) menu.Activator;
                var chosenCard = (DanmakuCharacterCardModel) menu.SessionChoices[0].SelectedTarget;
                
                // Todo: Assign the character card to the player
                
                
                
            }
        }
    }
}