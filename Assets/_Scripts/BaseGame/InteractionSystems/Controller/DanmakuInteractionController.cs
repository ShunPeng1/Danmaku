using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.InteractionSystems.Setups;
using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Setups;
using _Scripts.CoreGame.InteractionSystems.Stats;
using Shun_Utilities;
using UnityEditor;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuInteractionController
    {
        public DanmakuInteractionViewRepo InteractionViewRepo { get; private set; }
        
        // Controllers
        public DanmakuPlayerController DanmakuPlayerController { get; private set; }
        public DanmakuBoardController DanmakuBoardController { get; private set; }
        public DanmakuCombatController DanmakuCombatController { get; private set; }
        
        // Models
        public DanmakuBoardModel BoardModel { get; private set; }
        public DanmakuPlayerGroupModel PlayerGroupModel { get; private set; }
        
        private DanmakuInteractionController(DanmakuInteractionViewRepo danmakuInteractionViewRepo, DanmakuPlayerGroupModel playerGroupModel, DanmakuBoardModel boardModel)
        {
            InteractionViewRepo = danmakuInteractionViewRepo;
            PlayerGroupModel = playerGroupModel;
            BoardModel = boardModel;
            
            // Controllers
            
            var boardController = new DanmakuBoardController(this);
            var playerSubController = new DanmakuPlayerController(this);
            var combatController = new DanmakuCombatController(this);
        }
        
        public class Builder
        {
            private DanmakuInteractionViewRepo _viewRepo;
            private DanmakuPlayerGroupModel _playerGroupModel = new (
                new List<DanmakuPlayerModel>(){new DanmakuPlayerModel(0)}
                );
            private DanmakuBoardModel _boardModel = new DanmakuBoardModel(
                new DanmakuCardDeckModel(new List<IDanmakuCard>(){}),
                new DanmakuCardDeckModel(new List<IDanmakuCard>(){}),
                new DanmakuCardDeckModel(new List<IDanmakuCard>(){})
                );
            
            
            public Builder(DanmakuInteractionViewRepo viewRepo)
            {
                _viewRepo = viewRepo;
            }
            public Builder WithPlayerCount(int playerCount)
            {
                List<DanmakuPlayerModel> players = new List<DanmakuPlayerModel>();

                for (int i = 0; i < playerCount; i++)
                {
                    var player = new DanmakuPlayerModel(i);
                    players.Add(player);   
                }
                
                _playerGroupModel = new DanmakuPlayerGroupModel(players);
                _viewRepo.CreatePlayerViews(players);
                
                return this;
            }

            public Builder WithPlayerRoles(RoleSetConfig roleSetConfig)
            {
                DanmakuRoleSetupDirector roleSetupDirector = new DanmakuRoleSetupDirector(_playerGroupModel, _playerGroupModel.Players, roleSetConfig);
            
                var playerToRole = roleSetupDirector.SetupRoles();
                _viewRepo.SetupPlayerRoleView(playerToRole);

                return this;
            }
            
            public Builder WithCardDeck(DeckSetConfig deckSetConfig)
            {
                List<IDanmakuCard> mainDeck = new ();
                List<IDanmakuCard> discardDeck = new ();
                List<IDanmakuCard> incidentDeck = new ();
                
                DanmakuCardDeckModel mainDeckModel = new DanmakuCardDeckModel(mainDeck);
                DanmakuCardDeckModel discardDeckModel = new DanmakuCardDeckModel(discardDeck);
                DanmakuCardDeckModel incidentDeckModel = new DanmakuCardDeckModel(incidentDeck);
                

                DanmakuCardRuleFactory cardFactory = new ();
                foreach (var deckCardData in deckSetConfig.DeckCardsData)
                {
                    List<DanmakuCardRuleBase> cardRules = new ();
                    foreach (var cardRuleData in deckCardData.CardRulesScriptableData)
                    {
                        var cardRule = cardFactory.GetIDanmakuCardRule(cardRuleData);
                        
                        cardRules.Add(cardRule);
                    }

                    var mainDeckCardModel = new DanmakuMainDeckCardModel(deckCardData, cardRules, mainDeckModel);
                    mainDeck.Add(mainDeckCardModel);
                }

                mainDeck.Shuffle();
                incidentDeck.Shuffle();
                
                _boardModel = new DanmakuBoardModel(mainDeckModel, discardDeckModel, incidentDeckModel);
                
                _viewRepo.SetupMainDeck(mainDeckModel, discardDeckModel);
                _viewRepo.SetupIncidentDeck(incidentDeckModel);
                
                return this;
            }
            
            
            public DanmakuInteractionController Build()
            {
                return new DanmakuInteractionController(_viewRepo, _playerGroupModel, _boardModel);
            }
            
        }

        public void SetupStartingStats(StartupStatsConfig startupStatsConfig)
        {
            foreach (var player in PlayerGroupModel.Players)
            {
                player.InitializeStats(
                    new PlayerStat(startupStatsConfig.StartingHealth, 0, startupStatsConfig.MaxHealth),
                    new PlayerStat(startupStatsConfig.StartingHandSize, 0, startupStatsConfig.MaxHandSize),
                    new PlayerStat(startupStatsConfig.StartingDistant, startupStatsConfig.MinDistant),
                    new PlayerStat(startupStatsConfig.StartingRange, startupStatsConfig.MinRange),
                    new PlayerStat(startupStatsConfig.StartingPower, startupStatsConfig.MinPower, startupStatsConfig.MaxPower),
                    new PlayerStat(startupStatsConfig.CardDrawPerTurn, startupStatsConfig.MinCardDrawPerTurn, startupStatsConfig.MaxCardDrawPerTurn),
                    new PlayerStat(0,0, startupStatsConfig.MaxDanmakuCardPlayedPerTurn),
                    new PlayerStat(0, 0, startupStatsConfig.MaxSpellCardPlayedPerTurn)
                );
                
            }
        }


        public void StartupReveal()
        {
            DanmakuPlayerController.StartupReveal();
        }
        
        public void StartGame()
        {
            DanmakuPlayerController.StartGame();
        }
        
        public void StartPlayerStep()
        {
            DanmakuPlayerController.StartPlayerStep();
        }
        
    }
}