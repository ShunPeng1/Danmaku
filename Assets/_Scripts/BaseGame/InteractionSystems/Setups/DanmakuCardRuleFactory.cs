using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems.CardRules;

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
            if (_cardRuleFactories.TryGetValue(cardRuleData.CardRule, out var factory))
            {
                return factory();
            }
            throw new ArgumentException($"CardRule {cardRuleData.CardRuleName} not found");
        }
    }
}