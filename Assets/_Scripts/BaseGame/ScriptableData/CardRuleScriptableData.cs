using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.BaseGame.ScriptableData
{
    [CreateAssetMenu(fileName = "CardRuleScriptableData", menuName = "ScriptableData/CardRuleScriptableData")]
    public class CardRuleScriptableData : ScriptableObject
    {
        [TextArea(3, 10)]
        public string CardRule;
        public CardTimingTypeEnum CardTimingType;
        public CardEffectTypeEnum[] CardEffectTypes;
    }
}