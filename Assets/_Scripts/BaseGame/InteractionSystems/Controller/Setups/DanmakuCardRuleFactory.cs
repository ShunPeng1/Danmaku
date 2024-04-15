using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.CardRules;
using UnityEngine;

namespace _Scripts.BaseGame.InteractionSystems.Setups
{
    public class DanmakuCardRuleFactory
    {
        private readonly Dictionary<string, Func<CardRuleScriptableData, DanmakuCardRuleBase>> _cardRuleFactories = new()
            {
                {"MockCardRule", (cardRuleData) => new MockCardRule(cardRuleData)}
            };
        
        public DanmakuCardRuleBase GetIDanmakuCardRule(CardRuleScriptableData cardRuleData)
        {
            if (_cardRuleFactories.TryGetValue(cardRuleData.CardRuleName, out var factory))
            {
                return factory(cardRuleData);
            }
            
            Debug.LogError($"CardRule {cardRuleData.CardRuleName} not found");
            return null;
        }
    }
}