using System;
using System.Collections.Generic;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Setups;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuStepController
    {
	    ISetupPlayerView _setupPlayerView;
        
        
        DanmakuPlayerGroupModel _playerGroupModel;
            

        private DanmakuStepController(DanmakuPlayerGroupModel playerGroupModel, ISetupPlayerView setupPlayerView)
        {
            _playerGroupModel = playerGroupModel;
            _setupPlayerView = setupPlayerView;
        }
        
        public class Builder
        {
            private DanmakuPlayerGroupModel _playerGroupModel;
            private ISetupPlayerView _setupPlayerView;
            
            public Builder WithPlayerGroupModel(DanmakuPlayerGroupModel playerGroupModel)
            {
                _playerGroupModel = playerGroupModel;
                return this;
            }
            
            public DanmakuStepController Build(ISetupPlayerView setupPlayerView)
            {
                return new DanmakuStepController(_playerGroupModel, setupPlayerView);
            }
            
        }
        
        public void SetupPlayerGroup(int playerCount, RoleSetConfig roleSetConfig)
        {
            List<DanmakuPlayerModel> players = new List<DanmakuPlayerModel>();
                
            for (int i = 0; i < playerCount; i++)
            {
                var player = new DanmakuPlayerModel();
                players.Add(player);   
            }
                
            _playerGroupModel = new DanmakuPlayerGroupModel(players);
                
            DanmakuRoleSetupDirector roleSetupDirector = new DanmakuRoleSetupDirector(_playerGroupModel, players, roleSetConfig);
                
            var playerToRole = roleSetupDirector.SetupRoles();
            _setupPlayerView.SetupPlayerRoleView(playerToRole);
            
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