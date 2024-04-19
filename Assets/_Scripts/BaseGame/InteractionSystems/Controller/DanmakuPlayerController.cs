using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.InteractionSystems.GameSteps;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Stats;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayerController
    {
        private readonly DanmakuInteractionController _danmakuInteractionController;
        
        // Views
        private DanmakuInteractionViewRepo InteractionViewRepo => _danmakuInteractionController.InteractionViewRepo;
        private DanmakuTurnBaseView TurnBaseView => _danmakuInteractionController.InteractionViewRepo.TurnView;

        // Models
        private DanmakuPlayerGroupModel PlayerGroupModel => _danmakuInteractionController.PlayerGroupModel;
        private DanmakuBoardModel BoardModel => _danmakuInteractionController.BoardModel;
        
        
        private readonly DanmakuPlayerStepContext _currentStepContext; 
        
        public DanmakuPlayerController(DanmakuInteractionController danmakuInteractionController)
        {
            _danmakuInteractionController = danmakuInteractionController;
            _currentStepContext = new DanmakuPlayerStepContext(_danmakuInteractionController);
        }
        
        
        public void StartupReveal()
        {
            var startPlayer = PlayerGroupModel.Players.FirstOrDefault(player => player.Role.HasRole(DanmakuRoleEnum.Heroine));

            if (startPlayer != null)
            {
                var role = startPlayer.Role.RevealRole();

                var playerView = InteractionViewRepo.GetPlayerView(startPlayer);
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
            DanmakuPlayerBaseView currentPlayerView = InteractionViewRepo.GetPlayerView(currentPlayerModel);
            switch (PlayerGroupModel.CurrentPlayStepEnum.Value)
            {
                case PlayStepEnum.InitiateStep:
                    _currentStepContext.SetStep(new DanmakuInitiatePlayerStep(),currentPlayerModel, currentPlayerView);
                    break;
                case PlayStepEnum.IncidentStep:
                    _currentStepContext.SetStep(new DanmakuIncidentPlayerStep(),currentPlayerModel, currentPlayerView);
                    break;
                case PlayStepEnum.DrawStep:
                    _currentStepContext.SetStep(new DanmakuDrawPlayerStep(),currentPlayerModel, currentPlayerView);
                    break;
                case PlayStepEnum.MainStep:
                    _currentStepContext.SetStep(new DanmakuMainPlayerStep(),currentPlayerModel, currentPlayerView);
                    break;
                case PlayStepEnum.DiscardStep:
                    _currentStepContext.SetStep(new DanmakuDiscardPlayerStep(),currentPlayerModel, currentPlayerView);
                    break; 
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _currentStepContext.ExecuteStep(EndPlayerStep);
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
            
            TurnBaseView.EndPlayerStep(PlayerGroupModel.CurrentPlayerTurn.Value,PlayerGroupModel.CurrentPlayStepEnum.Value);

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
                    break; // No need to execute a step after setting the next player's turn
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            StartPlayerStep();
        }

        private void SetPlayerStep(PlayStepEnum playStepEnum)
        {
            PlayerGroupModel.CurrentPlayStepEnum.Value = playStepEnum; 
            TurnBaseView.StartPlayerStep(PlayerGroupModel.CurrentPlayerTurn.Value,PlayerGroupModel.CurrentPlayStepEnum.Value);
        }
        
        private void SetPlayerTurn(DanmakuPlayerModel startingPlayer)
        {
            SetCurrentTurnPlayer(startingPlayer);
            TurnBaseView.SetPlayerCurrentTurn(startingPlayer);
            
            SetPlayerStep(PlayStepEnum.InitiateStep);
        }
        
        private void SetPlayerNextTurn()
        {
            var nextPlayerModel = SetNextPlayerTurn();
            TurnBaseView.SetPlayerCurrentTurn(nextPlayerModel);
            
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