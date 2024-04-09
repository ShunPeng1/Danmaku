namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCardRule
    {
        
        public void InitializeCard();
        public bool CanExecuteRule(IDanmakuTargeter[] targeters, IDanmakuTargetable[] targetables);
        public void ExecuteRule(IDanmakuTargeter[] targeters, IDanmakuTargetable[] targetables);
        
    }
}