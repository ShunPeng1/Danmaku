using System;
using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public interface IDanmakuPlayerStep
    {   
        public bool CanEndStep(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView);
        public void Execute(DanmakuInteractionController interactionController, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView, Action finishExecuteCallback = null);
    }
}