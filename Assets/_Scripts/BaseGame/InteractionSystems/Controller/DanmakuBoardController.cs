using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views;
using _Scripts.BaseGame.Views.Basics;
using Shun_Utilities;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuBoardController
    {
        private readonly DanmakuInteractionController _interactionController;
        
        // Views
        private DanmakuInteractionViewRepo InteractionViewRepo => _interactionController.InteractionViewRepo;
        
        // Models
        private DanmakuPlayerGroupModel PlayerGroupModel => _interactionController.PlayerGroupModel;
        private DanmakuBoardModel BoardModel => _interactionController.BoardModel;
        
        
        public DanmakuBoardController(DanmakuInteractionController interactionController)
        {
            _interactionController = interactionController;
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
            player.DeckCardHandModel.AddCard(card);
            
            var boardView = _interactionController.InteractionViewRepo.BoardView;
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
                player.DeckCardHandModel.AddCard(card);
            }
            
            var boardView = _interactionController.InteractionViewRepo.BoardView;
            boardView.DrawCardFromMainDeck(player, cards);

        }

        public void StartDrawCharacter(int eachPlayerCharacterChoiceCount)
        {
            var menus = new List<DanmakuSessionMenu>();
            
            var session = new DanmakuSession.Builder()
                .WithPlayingPlayerModel(PlayerGroupModel.Players.ConvertAll(player => (IDanmakuActivator) player))
                .WithPlayerSessionKindEnum(EndSessionKindEnum.AllPlayed)
                .WithPlayingSessionMenus(menus)
                .WithCountDownTime(30f)
                .Build(_interactionController);

            
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
                
                DanmakuSessionMenu menu = new DanmakuSessionMenu(session, player, sessionChoices, ChoiceActionEnum.AutoCheck);
                
                sessionChoices.Add(new DanmakuSessionChoice(menu, characterCardChoices));
                
                menus.Add(menu);
                
                // Draw the character cards for selection
                InteractionViewRepo.BoardView.DrawCharacterCardsForSelection(player, characterCards);
                
            }
            
            // Remove the session from the player when the session ends
            session.OnSessionStartEvent.Subscribe(InteractionViewRepo.BoardView.AddSessionToPlayer);
            
            session.SubscribeOnSessionEnd(AssignCharacterCard, true);
            session.SubscribeOnSessionEnd(InteractionViewRepo.BoardView.RemoveSessionFromPlayer, true);
            
            session.OnFinallyEndSessionEvent.Subscribe(_interactionController.StartNextSequence);

            // Start the session
            session.StartSession();
        }

        private void AssignCharacterCard(DanmakuSession session)
        {
            List<DanmakuSessionMenu> danmakuSessionMenus = session.GetSessionMenus();
            foreach (var menu in danmakuSessionMenus)
            {
                var player = (DanmakuPlayerModel) menu.Activator;
                var chosenCard = (DanmakuCharacterCardModel) menu.SessionChoices[0].SelectedTarget;
                
                player.CharacterCardHandModel.AddCard(chosenCard);
                
                InteractionViewRepo.BoardView.DiscardCharacterCardForSelection(player);
                InteractionViewRepo.GetPlayerView(player).CharacterView.SetupCharacter(chosenCard);
            }
        }
    }
}