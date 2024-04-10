using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuPlayerStepContext
    {
        private IDanmakuPlayerStep _step;
        private DanmakuPlayerModel _playerModel;
        private DanmakuPlayerBaseView _playerView;


        public void SetStep(IDanmakuPlayerStep step, DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            _step = step;
            _playerModel = playerModel;
            _playerView = playerView;
        }

        public bool CanEndStep()
        {
            return _step.CanEndStep(_playerModel, _playerView);
        }
        
        public void ExecuteStep()
        {
            _step.Execute(_playerModel, _playerView);
        }
    }
}