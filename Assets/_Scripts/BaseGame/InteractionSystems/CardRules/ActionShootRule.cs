using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.CardRules.CombatUltility;

namespace _Scripts.CoreGame.InteractionSystems.CardRules
{
    [DanmakuCardRuleClass]
    public class ActionShootRule : DanmakuCardRuleBase
    {
        public ActionShootRule(CardRuleScriptableData cardRuleData, IDanmakuCard card, DanmakuInteractionController interactionController) : base(cardRuleData, card, interactionController)
        {
        }

        public override void InitializeCard()
        {
            
        }

        public override List<List<IDanmakuTargetable>> GetAnyValidTargetables(IDanmakuActivator danmakuActivator)
        {
            if (danmakuActivator is not DanmakuPlayerModel attackerPlayer)
            {
                return null;
            }
            
            List<List<IDanmakuTargetable>> allPossibleTargetables = new ();
            foreach (var targetPlayerModel in InteractionController.PlayerGroupModel.Players)
            {
                List<IDanmakuTargetable> targetables = new ();

                if (AttackCombatUtility.CanAttackInRange(InteractionController.PlayerGroupModel, attackerPlayer, targetPlayerModel))
                {
                    targetables.Add(targetPlayerModel);
                    allPossibleTargetables.Add(targetables);
                }            
            }
            
            return allPossibleTargetables;
            
        }

        public override bool CanExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            if (targetables is not { Count: 1 })
            {
                return false;
            }
            
            return targetables[0] is DanmakuPlayerModel playerModel && AttackCombatUtility.CanAttackInRange(InteractionController.PlayerGroupModel, activator as DanmakuPlayerModel, playerModel);
        }

        public override void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            if (targetables is not { Count: 1 })
            {
                return;
            }
            
            if (activator is DanmakuPlayerModel attackerPlayerModel && targetables[0] is DanmakuPlayerModel targetedPlayerModel)
            {
                AttackCombatUtility.AttackPlayer(attackerPlayerModel, targetedPlayerModel);
            }
        }
    }
}