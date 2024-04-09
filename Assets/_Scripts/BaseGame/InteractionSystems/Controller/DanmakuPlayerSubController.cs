using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.InteractionSystems.DanmakuGameState;
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
        
        public  DanmakuPlayerSubController(DanmakuInteractionController danmakuInteractionController)
        {
            _danmakuInteractionController = danmakuInteractionController;
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
            }
            else // if no heroine player, start with first player
            {
                SetPlayerTurn(PlayerGroupModel.Players[0]);
            }
            
        }
        
        public void StartPlayerNextStep()
        {
            switch (PlayerGroupModel.CurrentPlayStepEnum.Value)
            {
                case PlayStepEnum.SetupStep:
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.StartOfTurnStep;
                    break;
                case PlayStepEnum.StartOfTurnStep:
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.IncidentStep;
                    break;
                case PlayStepEnum.IncidentStep:
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.DrawStep;
                    break;
                case PlayStepEnum.DrawStep:
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.MainStep;
                    break;
                case PlayStepEnum.MainStep:
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.DiscardStep;
                    break;
                case PlayStepEnum.DiscardStep:
                    PlayerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.EndOfTurnStep;
                    break;
                case PlayStepEnum.EndOfTurnStep:
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void SetPlayerTurn(DanmakuPlayerModel startingPlayer)
        {
            PlayerGroupModel.SetCurrentTurnPlayer(startingPlayer);
            DanmakuTurnBaseView.SetPlayerCurrentTurn(startingPlayer);

        }
        
        private void SetPlayerNextTurn()
        {
            var nextPlayerModel = PlayerGroupModel.SetNextPlayerTurn();
            DanmakuTurnBaseView.SetPlayerCurrentTurn(nextPlayerModel);
        }
    }
}