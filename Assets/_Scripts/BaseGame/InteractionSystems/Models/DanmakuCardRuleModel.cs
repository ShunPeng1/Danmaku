using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuCardRuleModel
    {
        public IDanmakuCardRule DanmakuCardRule { get; private set; }
        public CardRuleScriptableData CardRuleScriptableData { get; private set; }
        
        public DanmakuCardRuleModel(CardRuleScriptableData cardRuleData,IDanmakuCardRule danmakuCardRule)
        {
            CardRuleScriptableData = cardRuleData;
            DanmakuCardRule = danmakuCardRule;
        }
        
    }
}