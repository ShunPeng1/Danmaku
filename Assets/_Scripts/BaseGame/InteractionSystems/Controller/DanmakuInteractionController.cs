using System;
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
        public DanmakuTurnController TurnController { get; private set; }
        public DanmakuBoardController BoardController { get; private set; }
        public DanmakuCombatController CombatController { get; private set; }
        
        // Models
        public DanmakuBoardModel BoardModel { get; private set; }
        public DanmakuPlayerGroupModel PlayerGroupModel { get; private set; }
        
        private List<Action> _startGameSequences = new();
        private int _currentSequenceIndex = -1;

        private DanmakuInteractionController(DanmakuInteractionViewRepo danmakuInteractionViewRepo,
            DanmakuPlayerGroupModel playerGroupModel, DanmakuBoardModel boardModel)
        {
            InteractionViewRepo = danmakuInteractionViewRepo;
            PlayerGroupModel = playerGroupModel;
            BoardModel = boardModel;

            // Controllers

            BoardController = new DanmakuBoardController(this);
            TurnController = new DanmakuTurnController(this);
            CombatController = new DanmakuCombatController(this);
        }
        private void UpdateStartGameSequences(List<Action> startGameSequences)
        {
            _startGameSequences = startGameSequences;
        }
        
        private void UpdatePlayerGroupModel(DanmakuPlayerGroupModel playerGroupModel)
        {
            PlayerGroupModel = playerGroupModel;
        }
        
        private void UpdateBoardModel(DanmakuBoardModel boardModel)
        {
            BoardModel = boardModel;
        }
        
        public class Builder
        {
            private DanmakuInteractionViewRepo _viewRepo;
            private DanmakuPlayerGroupModel _playerGroupModel = new (
                new List<DanmakuPlayerModel>(){new DanmakuPlayerModel(0)}
                );
            private DanmakuBoardModel.Builder _boardModelBuilder = new(
                new DanmakuCardDeckModel(new List<IDanmakuCard>(){}
                ));
            
            private readonly DanmakuInteractionController _interactionController;
            
            private List<Action> _startGameSequences = new();
            
            public Builder(DanmakuInteractionViewRepo viewRepo)
            {
                _viewRepo = viewRepo;
                _interactionController = new DanmakuInteractionController(viewRepo, _playerGroupModel, _boardModelBuilder.Build());
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
            
            public Builder WithCharacterSet(CharacterSetConfig characterSetConfig)
            {
                List<IDanmakuCard> characterCards = new();
                DanmakuCardDeckModel characterDeckModel = new DanmakuCardDeckModel(characterCards);
                
                foreach (var characterCardData in characterSetConfig.CharacterCardsData)
                {
                    var rules = new List<DanmakuCardRuleBase>();
                    var card = new DanmakuCharacterCardModel(characterCardData, rules, characterDeckModel);
                    
                    characterCards.Add(card);
                }
                characterCards.Shuffle();
                
                _boardModelBuilder.SetCharacterDeck(characterDeckModel);
                
                _viewRepo.SetupCharacterDeck(characterDeckModel);

                return this;
            }
            
            public Builder WithCardDeck(DeckSetConfig deckSetConfig)
            {
                List<IDanmakuCard> mainDeck = new ();
                
                DanmakuCardDeckModel mainDeckModel = new DanmakuCardDeckModel(mainDeck);
                
                _boardModelBuilder = new DanmakuBoardModel.Builder(mainDeckModel);

                DanmakuCardRuleFactory cardFactory = new (_interactionController);
                foreach (var deckCardData in deckSetConfig.DeckCardsData)
                {
                    List<DanmakuCardRuleBase> cardRules = new();
                    var mainDeckCardModel = new DanmakuMainDeckCardModel(deckCardData, cardRules, mainDeckModel);

                    foreach (var cardRuleData in deckCardData.CardRulesScriptableData)
                    {
                        var cardRule = cardFactory.CreateIDanmakuCardRule(cardRuleData, mainDeckCardModel);
                        
                        cardRules.Add(cardRule);
                    }

                    mainDeck.Add(mainDeckCardModel);
                }

                mainDeck.Shuffle();
                
                _viewRepo.SetupMainDeck(mainDeckModel);
                
                return this;
            }
            
            public Builder WithStartGameSequence(List<Action> startGameSequence)
            {
                _startGameSequences = startGameSequence;
                return this;
            }
            
            
            public DanmakuInteractionController Build()
            {
                _interactionController.UpdatePlayerGroupModel(_playerGroupModel);
                _interactionController.UpdateBoardModel(_boardModelBuilder.Build());
                _interactionController.UpdateStartGameSequences(_startGameSequences);
                return _interactionController;
            }
            
        }
        
        public void StartDrawCharacter(int eachPlayerCharacterChoiceCount)
        {
            BoardController.StartDrawCharacter(eachPlayerCharacterChoiceCount);
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
            
            StartNextSequence();
        }
        
        public void StartupDraw()
        {
            BoardController.StartupDraw();
            
            StartNextSequence();
        }
        

        public void StartupReveal()
        {
            TurnController.StartupReveal();
            
            StartNextSequence();
        }
        
        public void StartGame()
        {
            TurnController.StartGame();
        }

        public void StartNextSequence()
        {
            if (_currentSequenceIndex < _startGameSequences.Count)
            {
                _currentSequenceIndex++;
                _startGameSequences[_currentSequenceIndex]();
                
            }
        }
    }
}