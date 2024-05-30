using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.CardRules.CombatUltility;

namespace _Scripts.CoreGame.InteractionSystems.CardRules
{
    [DanmakuCardRuleClass]
    public class Action1UpRule : DanmakuCardRuleBase
    {
        public Action1UpRule(CardRuleScriptableData cardRuleData, IDanmakuCard card, DanmakuInteractionController interactionController) : base(cardRuleData, card, interactionController)
        {
        }
        
        public override void InitializeCard()
        {
            
        }

        public override List<TargetableQueryResult> GetAnyValidTargetables(IDanmakuActivator danmakuActivator)
        {
            List<TargetableQueryResult> allPossibleTargetables = new ();
            List<IDanmakuTargetable> targetables = new ();
            foreach (var playerModel in InteractionController.PlayerGroupModel.Players)
            {
                if (LifeCombatUtility.CanHeal(playerModel))
                {
                    targetables.Add(playerModel);
                }            
            }
            allPossibleTargetables.Add(new TargetableQueryResult(TargetableTypeEnum.Player, targetables));

            return allPossibleTargetables;
        }

        public override bool CanPlayRule(IDanmakuActivator activator)
        {
            return activator is DanmakuPlayerModel attackerPlayerModel;
        }

        public override bool CanExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            if (targetables is not { Count: 1 } || CardRuleScriptableData.CardRuleValueWithIcons.Length != 1)
            {
                return false;
            }
            
            return targetables[0] is DanmakuPlayerModel playerModel && LifeCombatUtility.CanHeal(playerModel);
        }

        public override void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            if (CanExecuteRule(activator, targetables) == false)
            {
                return;
            }
            
            var healAmount = CardRuleScriptableData.CardRuleValueWithIcons[0].Value;
            var playerModel = targetables[0] as DanmakuPlayerModel;
            
            playerModel.Life.Increase(healAmount);
        
        }
    }
}