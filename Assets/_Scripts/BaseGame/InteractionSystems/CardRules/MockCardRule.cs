using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.CardRules
{
    [DanmakuCardRuleClass]
    public class MockCardRule : DanmakuCardRuleBase
    {
        public MockCardRule(CardRuleScriptableData cardRuleData, IDanmakuCard card, DanmakuInteractionController interactionController) : base(cardRuleData, card, interactionController)
        {
        }
        
        public override void InitializeCard()
        {
            Debug.Log("MockCardRule InitializeCard");
        }

        public override List<List<IDanmakuTargetable>> GetAnyValidTargetables(IDanmakuActivator danmakuActivator)
        {
            return null;
        }

        public override bool CanExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables)
        {
            return true;
        }

        public override void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables)
        {
            Debug.Log("MockCardRule ExecuteRule");
        }
    }
}