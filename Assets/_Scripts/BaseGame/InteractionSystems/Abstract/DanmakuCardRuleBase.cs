using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;

namespace _Scripts.CoreGame.InteractionSystems
{
    public abstract class DanmakuCardRuleBase : IDanmakuCardRule
    {
        public IDanmakuCard Card { get;}
        public CardRuleScriptableData CardRuleScriptableData { get; private set; }
        
        public DanmakuCardRuleBase(CardRuleScriptableData cardRuleData, IDanmakuCard card)
        {
            CardRuleScriptableData = cardRuleData;
            Card = card;
        }
        
        public abstract void InitializeCard();

        public IDanmakuActivator GetAnyValidActivator()
        {
            return Card.GetCardOwner();
        }
        public abstract List<List<IDanmakuTargetable>> GetAnyValidTargetables();

        public abstract bool CanExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null);
        public abstract void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null);
    }
}