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
        
        public MockCardRule(CardRuleScriptableData cardRuleData) : base(cardRuleData)
        {
        }
        
        public override void InitializeCard()
        {
            Debug.Log("MockCardRule InitializeCard");
        }

        public override bool CanExecuteRule(List<IDanmakuActivator> activators, List<IDanmakuTargetable> targetables)
        {
            Debug.Log("MockCardRule CanExecuteRule");
            return true;
        }

        public override void ExecuteRule(List<IDanmakuActivator> activators, List<IDanmakuTargetable> targetables)
        {
            
            Debug.Log("MockCardRule ExecuteRule");
        }
        
    }
}