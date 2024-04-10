namespace _Scripts.CoreGame.InteractionSystems.GameSteps
{
    public interface IDanmakuPlayerStep
    {
        public bool CanEndStep(DanmakuPlayerModel player);
        public void Execute(DanmakuPlayerModel player);
    }
}