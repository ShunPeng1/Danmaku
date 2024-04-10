using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuDiscardPlayerStep : IDanmakuPlayerStep
    {
        public bool CanEndStep(DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            return playerModel.CardHandModel.Cards.Count <= playerModel.HandSize.Get();
        }

        public void Execute(DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            if (playerModel.CardHandModel.Cards.Count > playerModel.HandSize.Get())
            {
                
            }
        }
    }
}