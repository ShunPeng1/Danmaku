using System;
using _Scripts.BaseGame.Views.Abstracts.Visualizers;
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
        
        public DanmakuCardExecutionBaseVisualizer VisualizerPrefab;
        
        [ShowInInspector][ReadOnly][TextArea(10,20)]
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
                _finalRuleText = CardRule;
                return;
            }
            
            string[] ruleTexts = new string[CardRuleValueWithIcons.Length];
            for (int i = 0; i < CardRuleValueWithIcons.Length; i++)
            {
                if (CardRuleValueWithIcons[i].CardRuleTextIconEnum == CardRuleTextIconEnum.Empty)
                {
                    ruleTexts[i] = $"{CardRuleValueWithIcons[i].Value}";
                }
                else if (CardRuleValueWithIcons[i].Value < 0)
                {
                    ruleTexts[i] = $"{CardRuleValueWithIcons[i].CardRuleTextIconEnum}";
                }
                else
                {
                    if (CardRuleValueWithIcons[i].CardRuleTextIconEnum == CardRuleTextIconEnum.Card && CardRuleValueWithIcons[i].Value > 1)
                    {
                        ruleTexts[i] = $"{CardRuleValueWithIcons[i].Value} {CardRuleValueWithIcons[i].CardRuleTextIconEnum}s";
                    }
                    else
                    {
                        ruleTexts[i] = $"{CardRuleValueWithIcons[i].Value} {CardRuleValueWithIcons[i].CardRuleTextIconEnum}";
                    }
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