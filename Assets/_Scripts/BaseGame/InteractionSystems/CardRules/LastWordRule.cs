using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.CardRules.CombatUltility;

namespace _Scripts.CoreGame.InteractionSystems.CardRules
{
    [DanmakuCardRuleClass]
    public class LastWordRule : DanmakuCardRuleBase
    {
        public LastWordRule(CardRuleScriptableData cardRuleData, IDanmakuCard card, DanmakuInteractionController interactionController) : base(cardRuleData, card, interactionController)
        {
        }

        public override List<TargetableQueryResult> GetAnyValidTargetables(IDanmakuActivator danmakuActivator)
        {
            if (danmakuActivator is not DanmakuPlayerModel attackerPlayer)
            {
                return null;
            }
            
            List<TargetableQueryResult> allPossibleTargetables = new ();
            
            foreach (var targetPlayerModel in InteractionController.PlayerGroupModel.Players)
            {
                if (targetPlayerModel == attackerPlayer)
                {
                    continue;
                }
                
                List<IDanmakuTargetable> targetables = new () { targetPlayerModel };
                allPossibleTargetables.Add(new TargetableQueryResult(TargetableTypeEnum.Player, targetables));
            }
            //allPossibleTargetables.Add(new TargetableQueryResult(TargetableTypeEnum.Player, targetables));

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
            if (targetables == null)
            {
                return false;
            }

            foreach (var targetable in targetables)
            {
                if (targetable is not DanmakuPlayerModel)
                {
                    return false;
                }
            }

            return activator is DanmakuPlayerModel activatorPlayer;
        }

        public override void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            if (CanExecuteRule(activator, targetables) == false)
            {
                return;
            }
            
            var attackerPlayerModel = activator as DanmakuPlayerModel;

            foreach (var targetable in targetables)
            {
                if (targetable is DanmakuPlayerModel targetedPlayerModel)
                {
                    AttackCombatUtility.AttackPlayer(attackerPlayerModel, targetedPlayerModel);
                }
            }
            
            AttackCombatUtility.UseDanmakuCardPlayer(attackerPlayerModel);
        
        }
    
    }
}