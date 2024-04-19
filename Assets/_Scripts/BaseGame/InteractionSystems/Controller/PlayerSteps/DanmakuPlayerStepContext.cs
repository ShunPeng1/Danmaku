using System;
using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuPlayerStepContext
    {
        private DanmakuInteractionController _interactionController;
        
        private IDanmakuPlayerStep _step;
        private DanmakuPlayerModel _playerModel;
        private DanmakuPlayerBaseView _playerView;

        public DanmakuPlayerStepContext(DanmakuInteractionController danmakuInteractionController)
        {
            _interactionController = danmakuInteractionController;
        }

        public void SetStep(IDanmakuPlayerStep step, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            _step = step;
            _playerModel = playerModel;
            _playerView = playerView;
        }

        public bool CanEndStep()
        {
            return _step.CanEndStep(_interactionController, _playerModel, _playerView);
        }
        
        public void ExecuteStep(Action finishExecuteCallback = null)
        {
            _step.Execute(_interactionController, _playerModel, _playerView, finishExecuteCallback);
        }
    }
}