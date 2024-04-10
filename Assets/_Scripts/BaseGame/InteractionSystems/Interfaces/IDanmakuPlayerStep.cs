using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public interface IDanmakuPlayerStep
    {   
        public bool CanEndStep(DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView);
        public void Execute(DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView);
    }
}