namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public class DanmakuDiscardPlayerStep : IDanmakuPlayerStep
    {
        public bool CanEndStep(DanmakuPlayerModel player)
        {
            return player.CardHandModel.Cards.Count <= player.HandSize.Get();
        }

        public void Execute(DanmakuPlayerModel player)
        {
            if (player.CardHandModel.Cards.Count > player.HandSize.Get())
            {
                
            }
        }
    }
}