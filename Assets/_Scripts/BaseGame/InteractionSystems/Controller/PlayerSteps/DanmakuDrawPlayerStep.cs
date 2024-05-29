using System;
using _Scripts.BaseGame.Views;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuDrawPlayerStep : IDanmakuPlayerStep
    {
        public bool CanEndStep(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            return true;
        }

        public void Execute(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView, Action finishExecuteCallback = null)
        {
            Debug.Log(playerModel.PlayerId + " Draw Step Executed!");

            
            
            
            interactionController.BoardController.DrawCard(playerModel, playerModel.CardDrawIncomeCount.Get());
            
            
            finishExecuteCallback?.Invoke();
        }
    }
}