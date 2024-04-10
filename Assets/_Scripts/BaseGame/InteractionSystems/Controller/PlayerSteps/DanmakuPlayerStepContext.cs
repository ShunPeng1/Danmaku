namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuPlayerStepContext
    {
        private IDanmakuPlayerStep _step;
        private DanmakuPlayerModel _player;


        public void SetStep(IDanmakuPlayerStep step, DanmakuPlayerModel player)
        {
            _step = step;
            _player = player;
        }

        public bool CanEndStep(DanmakuPlayerModel player)
        {
            return _step.CanEndStep(player);
        }
        
        public void ExecuteStep(DanmakuPlayerModel player)
        {
            _step.Execute(player);
        }
    }
}