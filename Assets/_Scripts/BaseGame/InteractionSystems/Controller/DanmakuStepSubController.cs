using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Setups;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuStepSubController
    {
        DanmakuInteractionController _interactionController;
        DanmakuSetupPlayerBaseView _setupPlayerViewRepo;
        
        DanmakuPlayerGroupModel _playerGroupModel;
        
        private DanmakuStepSubController(DanmakuInteractionController danmakuInteractionController, DanmakuPlayerGroupModel playerGroupModel, DanmakuSetupPlayerBaseView setupPlayerViewRepo)
        {
            _interactionController = danmakuInteractionController;
            _playerGroupModel = playerGroupModel;
            _setupPlayerViewRepo = setupPlayerViewRepo;
        }
        
        public class Builder
        {
            private DanmakuPlayerGroupModel _playerGroupModel = new (new List<DanmakuPlayerModel>(){new DanmakuPlayerModel(0)});
            
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
            
            public DanmakuStepSubController Build(DanmakuInteractionController interactionController , DanmakuSetupPlayerBaseView setupPlayerView)
            {
                return new DanmakuStepSubController(interactionController, _playerGroupModel, setupPlayerView);
            }
            
        }
        
        public void SetupPlayerRole(RoleSetConfig roleSetConfig)
        {
            DanmakuRoleSetupDirector roleSetupDirector = new DanmakuRoleSetupDirector(_playerGroupModel, _playerGroupModel.Players, roleSetConfig);
            
            var playerToRole = roleSetupDirector.SetupRoles();
            _setupPlayerViewRepo.SetupPlayerRoleView(playerToRole);
            
        }
        
        public void StartGame()
        {
            _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.SetupStep;
            _playerGroupModel.CurrentPlayerTurnIndex.Value = 0;
            _playerGroupModel.CurrentPlayerTurn.Value = _playerGroupModel.Players[0];
        }
        
        public void StartPlayerNextStep()
        {
            switch (_playerGroupModel.CurrentPlayStepEnum.Value)
            {
                case PlayStepEnum.SetupStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.StartOfTurnStep;
                    break;
                case PlayStepEnum.StartOfTurnStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.IncidentStep;
                    break;
                case PlayStepEnum.IncidentStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.DrawStep;
                    break;
                case PlayStepEnum.DrawStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.MainStep;
                    break;
                case PlayStepEnum.MainStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.DiscardStep;
                    break;
                case PlayStepEnum.DiscardStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.EndOfTurnStep;
                    break;
                case PlayStepEnum.EndOfTurnStep:
                    StartPlayerTurn();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void StartPlayerTurn()
        {
            _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.SetupStep;
            _playerGroupModel.CurrentPlayerTurnIndex.Value++;
            _playerGroupModel.CurrentPlayerTurn.Value = _playerGroupModel.Players[0];
        }

    }
}