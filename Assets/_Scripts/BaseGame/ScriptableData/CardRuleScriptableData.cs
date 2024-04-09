using System;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.ScriptableData
{
    [CreateAssetMenu(fileName = "CardRuleScriptableData", menuName = "ScriptableData/CardRuleScriptableData")]
    public class CardRuleScriptableData : ScriptableObject
    {
        [DanmakuClassRuleProperty]
        public string CardRuleName;
        [TextArea(3, 10)]
        public string CardRule;
        public CardTimingTypeEnum CardTimingType;
        public CardEffectTypeEnum[] CardEffectTypes;
        public CardRuleValueWithIcon[] CardRuleValueWithIcons;
        
        [ShowInInspector][ReadOnly][TextArea(3, 10)]
        private string _finalRuleText;
        
        
        [Serializable]
        public class CardRuleValueWithIcon
        {
            public int Value;
            public CardRuleTextIconEnum CardRuleTextIconEnum;
        }

        private void OnValidate()
        {   
            if (CardRuleValueWithIcons == null || CardRuleValueWithIcons.Length == 0)
            {
                return;
            }
            
            string[] ruleTexts = new string[CardRuleValueWithIcons.Length];
            for (int i = 0; i < CardRuleValueWithIcons.Length; i++)
            {
                if (CardRuleValueWithIcons[i].Value <= 0)
                {
                    ruleTexts[i] = $"{CardRuleValueWithIcons[i].CardRuleTextIconEnum}";
                }
                else
                {
                    ruleTexts[i] = $"{CardRuleValueWithIcons[i].Value} {CardRuleValueWithIcons[i].CardRuleTextIconEnum}";

                }
            }
            
            object[] intValueObjects = new object[CardRuleValueWithIcons.Length];
            
            for (int i = 0; i < CardRuleValueWithIcons.Length; i++)
            {
                intValueObjects[i] = ruleTexts[i];
            }

            
            _finalRuleText = string.Format(CardRule, args: intValueObjects);
        }
        
           
    }
}