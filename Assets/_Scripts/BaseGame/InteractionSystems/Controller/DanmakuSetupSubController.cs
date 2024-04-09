using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.InteractionSystems.Setups;
using _Scripts.BaseGame.Views;
using _Scripts.BaseGame.Views.Default;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Setups;
using _Scripts.CoreGame.InteractionSystems.Stats;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuSetupSubController
    {
        DanmakuInteractionController _interactionController;
        DanmakuSetupPlayerBaseView _setupPlayerView;
        
        DanmakuPlayerGroupModel _playerGroupModel;
        DanmakuBoardModel _boardModel;
        
        private DanmakuSetupSubController(DanmakuInteractionController danmakuInteractionController, DanmakuPlayerGroupModel playerGroupModel, DanmakuBoardModel boardModel, DanmakuSetupPlayerBaseView setupPlayerViewRepo)
        {
            _interactionController = danmakuInteractionController;
            _playerGroupModel = playerGroupModel;
            _boardModel = boardModel;
            _setupPlayerView = setupPlayerViewRepo;
        }
        
        public class Builder
        {
            private DanmakuInteractionController _interactionController;
            private DanmakuPlayerGroupModel _playerGroupModel = new (
                new List<DanmakuPlayerModel>(){new DanmakuPlayerModel(0)}
                );
            private DanmakuBoardModel _boardModel = new DanmakuBoardModel(
                new DanmakuCardDeckModel(new List<IDanmakuCard>(){}),
                new DanmakuCardDeckModel(new List<IDanmakuCard>(){}),
                new DanmakuCardDeckModel(new List<IDanmakuCard>(){})
                );
            private DanmakuSetupPlayerBaseView _setupPlayerView;
            
            public Builder(DanmakuInteractionController interactionController, DanmakuSetupPlayerBaseView setupPlayerView)
            {
                _interactionController = interactionController;
                _setupPlayerView = setupPlayerView;
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
                _setupPlayerView.CreatePlayerViews(players);
                
                return this;
            }

            public Builder WithPlayerRoles(RoleSetConfig roleSetConfig)
            {
                DanmakuRoleSetupDirector roleSetupDirector = new DanmakuRoleSetupDirector(_playerGroupModel, _playerGroupModel.Players, roleSetConfig);
            
                var playerToRole = roleSetupDirector.SetupRoles();
                _setupPlayerView.SetupPlayerRoleView(playerToRole);

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
                    List<DanmakuCardRuleModel> cardRules = new ();
                    foreach (var cardRuleData in deckCardData.CardRulesScriptableData)
                    {
                        var cardRule = cardFactory.GetIDanmakuCardRule(cardRuleData);
                        var cardRuleModel = new DanmakuCardRuleModel(cardRuleData, cardRule);
                        cardRules.Add(cardRuleModel);
                    }

                    var mainDeckCardModel = new DanmakuMainDeckCardModel(deckCardData, cardRules, mainDeckModel);
                    mainDeck.Add(mainDeckCardModel);
                }

                _boardModel = new DanmakuBoardModel(mainDeckModel, discardDeckModel, incidentDeckModel);
                
                _setupPlayerView.SetupCardDeck(mainDeckModel, discardDeckModel, incidentDeckModel);
                
                return this;
            }
            
            
            public DanmakuSetupSubController Build()
            {
                return new DanmakuSetupSubController(_interactionController, _playerGroupModel, _boardModel, _setupPlayerView);
            }
            
        }

        public void SetupStartingStats(StartupStatsConfig startupStatsConfig)
        {
            
            foreach (var player in _playerGroupModel.Players)
            {
                player.InitializeStats(
                    new PlayerStat(startupStatsConfig.StartingHealth, 0, startupStatsConfig.MaxHealth),
                    new PlayerStat(startupStatsConfig.StartingHandSize, 0, startupStatsConfig.MaxHandSize),
                    new PlayerStat(startupStatsConfig.StartingDistant, startupStatsConfig.MinDistant),
                    new PlayerStat(startupStatsConfig.StartingRange, startupStatsConfig.MinRange),
                    new PlayerStat(startupStatsConfig.StartingPower, startupStatsConfig.MinPower, startupStatsConfig.MaxPower)
                );

            }
            
            
        }
    }
}