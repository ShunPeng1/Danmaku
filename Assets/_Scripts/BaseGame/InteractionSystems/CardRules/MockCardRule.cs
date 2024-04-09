using System;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.CardRules
{
    [DanmakuCardRuleClass]
    public class MockCardRule : IDanmakuCardRule
    {
        public void InitializeCard()
        {
            Debug.Log("MockCardRule InitializeCard");
        }

        public bool CanExecuteRule(IDanmakuTargeter[] targeters, IDanmakuTargetable[] targetables)
        {
            Debug.Log("MockCardRule CanExecuteRule");
            return true;
        }

        public void ExecuteRule(IDanmakuTargeter[] targeters, IDanmakuTargetable[] targetables)
        {
            Debug.Log("MockCardRule ExecuteRule");
        }
        
    }
}