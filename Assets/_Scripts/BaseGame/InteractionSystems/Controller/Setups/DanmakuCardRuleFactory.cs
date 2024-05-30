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
        private readonly Dictionary<string, Func<CardRuleScriptableData, IDanmakuCard, DanmakuCardRuleBase>> _cardRuleFactories;
        private readonly DanmakuInteractionController _interactionController;
        
        public DanmakuCardRuleFactory(DanmakuInteractionController interactionController)
        {
            _interactionController = interactionController;
            
            
            _cardRuleFactories = new Dictionary<string, Func<CardRuleScriptableData, IDanmakuCard, DanmakuCardRuleBase>>
            {
                {nameof(MockCardRule), (cardRuleData, card) => new MockCardRule(cardRuleData,card, _interactionController)},
                {nameof(Action1UpRule), (cardRuleData, card) => new Action1UpRule(cardRuleData,card, _interactionController)},
                {nameof(ActionShootRule), (cardRuleData, card) => new ActionShootRule(cardRuleData,card, _interactionController)},
                {nameof(GrimoireRule), (cardRuleData, card) => new GrimoireRule(cardRuleData,card, _interactionController)},
                {nameof(LazerShotRule), (cardRuleData, card) => new LazerShotRule(cardRuleData,card, _interactionController)},
            };
        }
        
        public DanmakuCardRuleBase CreateIDanmakuCardRule(CardRuleScriptableData cardRuleData, IDanmakuCard card)
        {
            if (_cardRuleFactories.TryGetValue(cardRuleData.CardRuleName, out var factory))
            {
                return factory(cardRuleData, card);
            }
            
            Debug.LogError($"CardRule {cardRuleData.CardRuleName} not found");
            return new MockCardRule(cardRuleData, card, _interactionController);
        }
    }
}