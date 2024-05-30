using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems.Attributes;

namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCardRule : IDanmakuTargetable
    {
        
        public abstract IDanmakuActivator GetAnyValidActivator();
        public abstract List<TargetableQueryResult> GetAnyValidTargetables(IDanmakuActivator danmakuActivator);
        
        public abstract bool CanPlayRule(IDanmakuActivator activator);
        public bool CanExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables);
        public void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables);
        
    }
}