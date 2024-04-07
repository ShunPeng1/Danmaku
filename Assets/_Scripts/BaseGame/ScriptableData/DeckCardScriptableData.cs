using System;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.CoreGame.Configurations
{
    
    [CreateAssetMenu(fileName = "DeckCardScriptableData", menuName = "ScriptableData/DeckCardScriptableData")]
    public class DeckCardScriptableData : ScriptableObject
    {
        public string CardName;
        public string CardTitle;
        public CardDeckEnum CardDeck;
        public CardExpansionEnum CardExpansion;
        public int PointValue;
        public CardRuleScriptableData[] CardRulesScriptableData;
        
        public Sprite CardIllustration;
        
        
    }


    [CreateAssetMenu(fileName = "CardRuleScriptableData", menuName = "ScriptableData/CardRuleScriptableData")]
    public class CardRuleScriptableData : ScriptableObject
    {
        [TextArea(3, 10)]
        public string CardRule;
        public CardTimingTypeEnum CardTimingType;
        public CardEffectTypeEnum[] CardEffectTypes;
    }
}