using System.Collections.Generic;

namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCardRule
    {
        
        public void InitializeCard();
        
        public abstract IDanmakuActivator GetAnyValidActivator();
        public abstract List<List<IDanmakuTargetable>> GetAnyValidTargetables();
        public bool CanExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables);
        public void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables);
        
    }
}