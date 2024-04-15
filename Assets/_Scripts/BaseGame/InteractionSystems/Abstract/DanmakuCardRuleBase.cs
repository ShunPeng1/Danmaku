using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;

namespace _Scripts.CoreGame.InteractionSystems
{
    public abstract class DanmakuCardRuleBase : IDanmakuCardRule
    {
        public CardRuleScriptableData CardRuleScriptableData { get; private set; }
        
        public DanmakuCardRuleBase(CardRuleScriptableData cardRuleData)
        {
            CardRuleScriptableData = cardRuleData;
        }
        
        public abstract void InitializeCard();
        public abstract bool CanExecuteRule(List<IDanmakuActivator> activators, List<IDanmakuTargetable> targetables);
        public abstract void ExecuteRule(List<IDanmakuActivator> activators, List<IDanmakuTargetable> targetables);
    }
}