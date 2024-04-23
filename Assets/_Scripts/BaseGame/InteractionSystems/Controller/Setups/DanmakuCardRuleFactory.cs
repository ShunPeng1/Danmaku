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
        private readonly Dictionary<string, Func<CardRuleScriptableData, IDanmakuCard, DanmakuCardRuleBase>> _cardRuleFactories = new()
            {
                {"MockCardRule", (cardRuleData, card) => new MockCardRule(cardRuleData,card)}
            };
        
        public DanmakuCardRuleBase CreateIDanmakuCardRule(CardRuleScriptableData cardRuleData, IDanmakuCard card)
        {
            if (_cardRuleFactories.TryGetValue(cardRuleData.CardRuleName, out var factory))
            {
                return factory(cardRuleData, card);
            }
            
            Debug.LogError($"CardRule {cardRuleData.CardRuleName} not found");
            return new MockCardRule(cardRuleData, card);
        }
    }
}