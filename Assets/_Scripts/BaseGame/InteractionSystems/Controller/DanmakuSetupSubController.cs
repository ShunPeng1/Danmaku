using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views;
using _Scripts.BaseGame.Views.Default;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Setups;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuSetupSubController
    {
        DanmakuInteractionController _interactionController;
        DanmakuSetupPlayerBaseView _setupPlayerViewRepo;
        
        DanmakuPlayerGroupModel _playerGroupModel;
        DanmakuBoardModel _boardModel;
        
        private DanmakuSetupSubController(DanmakuInteractionController danmakuInteractionController, DanmakuPlayerGroupModel playerGroupModel, DanmakuBoardModel boardModel, DanmakuSetupPlayerBaseView setupPlayerViewRepo)
        {
            _interactionController = danmakuInteractionController;
            _playerGroupModel = playerGroupModel;
            _boardModel = boardModel;
            _setupPlayerViewRepo = setupPlayerViewRepo;
        }
        
        public class Builder
        {
            private DanmakuPlayerGroupModel _playerGroupModel = new (new List<DanmakuPlayerModel>(){new DanmakuPlayerModel(0)});
            private DanmakuBoardModel _boardModel = new DanmakuBoardModel();
            private DanmakuSetupPlayerBaseView _setupPlayerView;
            public Builder()
            {
                
            }
            public Builder WithPlayerGroupModel(int playerCount, RoleSetConfig roleSetConfig)
            {
                List<DanmakuPlayerModel> players = new List<DanmakuPlayerModel>();

                for (int i = 0; i < playerCount; i++)
                {
                    var player = new DanmakuPlayerModel(i);
                    players.Add(player);   
                }
                
                _playerGroupModel = new DanmakuPlayerGroupModel(players);

                return this;
            }
            
            public Builder WithCardDeck(DeckSetConfig deckSetConfig)
            {
                
                
                return this;
            }
            
            
            public DanmakuSetupSubController Build(DanmakuInteractionController interactionController , DanmakuSetupPlayerBaseView setupPlayerView)
            {
                return new DanmakuSetupSubController(interactionController, _playerGroupModel, _boardModel, setupPlayerView);
            }
            
        }
        
        public void SetupPlayerRole(RoleSetConfig roleSetConfig)
        {
            DanmakuRoleSetupDirector roleSetupDirector = new DanmakuRoleSetupDirector(_playerGroupModel, _playerGroupModel.Players, roleSetConfig);
            
            var playerToRole = roleSetupDirector.SetupRoles();
            _setupPlayerViewRepo.SetupPlayerRoleView(playerToRole);
            
        }
        
        public void SetupCardDeck()
        {
            
        }
        
        
    }
}