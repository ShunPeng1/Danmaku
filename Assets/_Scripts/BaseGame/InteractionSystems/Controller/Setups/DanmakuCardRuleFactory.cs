using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems.CardRules;
using UnityEngine;

namespace _Scripts.BaseGame.InteractionSystems.Setups
{
    public class DanmakuCardRuleFactory
    {
        private readonly Dictionary<string, Func<IDanmakuCardRule>> _cardRuleFactories =
            new Dictionary<string, Func<IDanmakuCardRule>>()
            {
                {"MockCardRule", () => new MockCardRule()}
            };
        
        public IDanmakuCardRule GetIDanmakuCardRule(CardRuleScriptableData cardRuleData)
        {
            if (_cardRuleFactories.TryGetValue(cardRuleData.CardRuleName, out var factory))
            {
                return factory();
            }
            
            Debug.LogError($"CardRule {cardRuleData.CardRuleName} not found");
            return null;
        }
    }
}