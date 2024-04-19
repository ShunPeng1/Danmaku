using System.Collections.Generic;

namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCardRule
    {
        public void InitializeCard();
        public bool CanExecuteRule(List<IDanmakuActivator> activators, List<IDanmakuTargetable> targetables);
        public void ExecuteRule(List<IDanmakuActivator> activators, List<IDanmakuTargetable> targetables);
        
    }
}