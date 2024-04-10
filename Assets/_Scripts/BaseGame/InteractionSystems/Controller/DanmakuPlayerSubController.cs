using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.InteractionSystems.GameSteps;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Stats;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayerSubController
    {
        private DanmakuInteractionController _danmakuInteractionController;
        private DanmakuPlayerGroupModel PlayerGroupModel => _danmakuInteractionController.PlayerGroupModel;
        private DanmakuBoardModel BoardModel => _danmakuInteractionController.BoardModel;
        
        private DanmakuSetupPlayerBaseView SetupPlayerView => _danmakuInteractionController.InteractionViewRepo.SetupPlayerView;
        private DanmakuTurnBaseView DanmakuTurnBaseView => _danmakuInteractionController.InteractionViewRepo.TurnView;

        private DanmakuPlayerStepContext _currentStepContext; 
        
        public  DanmakuPlayerSubController(DanmakuInteractionController danmakuInteractionController)
        {
            _danmakuInteractionController = danmakuInteractionController;
            _currentStepContext = new DanmakuPlayerStepContext();
        }
        
        
        public void StartupReveal()
        {
            var startPlayer = PlayerGroupModel.Players.FirstOrDefault(player => player.Role.HasRole(DanmakuRoleEnum.Heroine));

            if (startPlayer != null)
            {
                var role = startPlayer.Role.RevealRole();

                var playerView = SetupPlayerView.GetPlayerView(startPlayer);
                var roleView = playerView.RoleView;
                roleView.RevealRole(role);
            }
        }
        
        
        public void StartGame()
        {
            var heroinePlayer = PlayerGroupModel.Players.FirstOrDefault(player => player.Role.HasRole(DanmakuRoleEnum.Heroine));

            if (heroinePlayer != null)
            {
                SetPlayerTurn(heroinePlayer);
                StartPlayerStep();
            }
            else // if no heroine player, start with first player
            {
                SetPlayerTurn(PlayerGroupModel.Players[0]);
                StartPlayerStep();
            }
            
        }
        
        public void StartPlayerStep()
        {
            DanmakuPlayerModel currentPlayerModel = PlayerGroupModel.CurrentPlayerTurn.Value;
            DanmakuPlayerBaseView currentPlayerView = SetupPlayerView.GetPlayerView(currentPlayerModel);
            switch (PlayerGroupModel.CurrentPlayStepEnum.Value)
            {
                case PlayStepEnum.InitiateStep:
                    _currentStepContext.SetStep(new DanmakuInitiatePlayerStep(),currentPlayerModel, currentPlayerView);
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.InitiateStep;
                    break;
                case PlayStepEnum.IncidentStep:
                    _currentStepContext.SetStep(new DanmakuIncidentPlayerStep(),currentPlayerModel, currentPlayerView);
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.IncidentStep;
                    break;
                case PlayStepEnum.DrawStep:
                    _currentStepContext.SetStep(new DanmakuDrawPlayerStep(),currentPlayerModel, currentPlayerView);
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.DrawStep;
                    break;
                case PlayStepEnum.MainStep:
                    _currentStepContext.SetStep(new DanmakuMainPlayerStep(),currentPlayerModel, currentPlayerView);
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.MainStep;
                    break;
                case PlayStepEnum.DiscardStep:
                    _currentStepContext.SetStep(new DanmakuDiscardPlayerStep(),currentPlayerModel, currentPlayerView);
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.DiscardStep;
                    return; // No need to execute a step after setting the next player's turn
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _currentStepContext.ExecuteStep();
        }
        
        public bool CanEndPlayerStep()
        {
            return _currentStepContext.CanEndStep();
        }
        
        public void EndPlayerStep()
        {
            if (!_currentStepContext.CanEndStep())
            {
                return;
            }
            
            DanmakuTurnBaseView.EndPlayerStep(PlayerGroupModel.CurrentPlayerTurn.Value,PlayerGroupModel.CurrentPlayStepEnum.Value);

            switch (PlayerGroupModel.CurrentPlayStepEnum.Value)
            {
                case PlayStepEnum.InitiateStep:
                    SetPlayerStep(PlayStepEnum.IncidentStep);
                    break;
                case PlayStepEnum.IncidentStep:
                    SetPlayerStep(PlayStepEnum.DrawStep);
                    break;
                case PlayStepEnum.DrawStep:
                    SetPlayerStep(PlayStepEnum.MainStep);
                    break;
                case PlayStepEnum.MainStep:
                    SetPlayerStep(PlayStepEnum.DiscardStep);
                    break;
                case PlayStepEnum.DiscardStep:
                    SetPlayerNextTurn();
                    return; // No need to execute a step after setting the next player's turn
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void SetPlayerStep(PlayStepEnum playStepEnum)
        {
            PlayerGroupModel.CurrentPlayStepEnum.Value = playStepEnum; 
            DanmakuTurnBaseView.StartPlayerStep(PlayerGroupModel.CurrentPlayerTurn.Value,PlayerGroupModel.CurrentPlayStepEnum.Value);
        }
        
        private void SetPlayerTurn(DanmakuPlayerModel startingPlayer)
        {
            SetCurrentTurnPlayer(startingPlayer);
            DanmakuTurnBaseView.SetPlayerCurrentTurn(startingPlayer);
            
            SetPlayerStep(PlayStepEnum.InitiateStep);
        }
        
        private void SetPlayerNextTurn()
        {
            var nextPlayerModel = SetNextPlayerTurn();
            DanmakuTurnBaseView.SetPlayerCurrentTurn(nextPlayerModel);
            
            SetPlayerStep(PlayStepEnum.InitiateStep);
        }


        private void SetCurrentTurnPlayer(DanmakuPlayerModel player)
        {
            PlayerGroupModel.CurrentPlayerTurn.Value = player;
            PlayerGroupModel.CurrentPlayerTurnIndex.Value = PlayerGroupModel.Players.IndexOf(player);
        }

        private DanmakuPlayerModel SetNextPlayerTurn()
        {
            int nextIndex = PlayerGroupModel.CurrentPlayerTurnIndex.Value + 1;
            if (nextIndex >= PlayerGroupModel.PlayerCount)
            {
                nextIndex = 0;
            }
            PlayerGroupModel.CurrentPlayerTurn.Value = PlayerGroupModel.Players[nextIndex];
            PlayerGroupModel.CurrentPlayerTurnIndex.Value = nextIndex;

            return PlayerGroupModel.CurrentPlayerTurn.Value;
        }

        
    }
}