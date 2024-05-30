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

        public override List<TargetableQueryResult> GetAnyValidTargetables(IDanmakuActivator danmakuActivator)
        {
            if (danmakuActivator is not DanmakuPlayerModel attackerPlayer)
            {
                return null;
            }
            
            List<TargetableQueryResult> allPossibleTargetables = new ();
            
            List<IDanmakuTargetable> targetables = new ();
            foreach (var targetPlayerModel in InteractionController.PlayerGroupModel.Players)
            {

                if (AttackCombatUtility.CanAttackInRange(InteractionController.PlayerGroupModel, attackerPlayer, targetPlayerModel))
                {
                    targetables.Add(targetPlayerModel);
                }            
            }
            allPossibleTargetables.Add(new TargetableQueryResult(TargetableTypeEnum.Player, targetables));

            return allPossibleTargetables;
            
        }

        public override bool CanPlayRule(IDanmakuActivator activator)
        {
            if (activator is DanmakuPlayerModel attackerPlayerModel)
            {
                return AttackCombatUtility.CanUseDanmakuCardPlayer(attackerPlayerModel);
                
            }
            
            return false;
        }

        public override bool CanExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            if (targetables is not { Count: 1 })
            {
                return false;
            }
            
            return activator is DanmakuPlayerModel activatorPlayer && targetables[0] is DanmakuPlayerModel targetedPlayer && AttackCombatUtility.CanAttackInRange(InteractionController.PlayerGroupModel,activatorPlayer , targetedPlayer);
        }

        public override void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            if (CanExecuteRule(activator, targetables) == false)
            {
                return;
            }
            
            var attackerPlayerModel = activator as DanmakuPlayerModel;
            var targetedPlayerModel = targetables[0] as DanmakuPlayerModel;
            
            AttackCombatUtility.AttackPlayer(attackerPlayerModel, targetedPlayerModel);
            AttackCombatUtility.UseDanmakuCardPlayer(attackerPlayerModel);
        
        }
    }
}