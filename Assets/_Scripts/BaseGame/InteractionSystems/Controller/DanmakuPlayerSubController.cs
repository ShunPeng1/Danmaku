using System;
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems.DanmakuGameState;
using _Scripts.CoreGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayerSubController
    {
        private DanmakuPlayerGroupModel _playerGroupModel;
        
        public  DanmakuPlayerSubController()
        {
            
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