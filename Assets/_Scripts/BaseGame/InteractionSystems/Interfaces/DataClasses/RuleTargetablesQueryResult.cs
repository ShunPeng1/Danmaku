using System.Collections.Generic;

namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public class RuleTargetablesQueryResult
    {
        public IDanmakuCardRule CardRule { get; set; }
        public IDanmakuActivator Activator { get; set; }
        public List<TargetableQueryResult> Targetables { get; set; }
        
        public RuleTargetablesQueryResult(IDanmakuCardRule cardRule, IDanmakuActivator activator, List<TargetableQueryResult> targetables)
        {
            CardRule = cardRule;
            Activator = activator;
            Targetables = targetables;
        }
    }
}