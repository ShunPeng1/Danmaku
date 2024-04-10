using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuInitiatePlayerStep : IDanmakuPlayerStep
    {
        
        public bool CanEndStep(DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            return false;
        }

        public void Execute(DanmakuPlayerModel playerModel, DanmakuPlayerBaseView playerView)
        {
            // Reset the player's card played counts
            playerModel.DanmakuCardPlayedCount.Set(0);
            playerModel.SpellCardPlayedCount.Set(0);

        }
    }
}