using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems.Attributes;

namespace _Scripts.CoreGame.InteractionSystems.CardRules
{
    [DanmakuCardRuleClass]
    public class GrimoireRule : DanmakuCardRuleBase
    {
        public GrimoireRule(CardRuleScriptableData cardRuleData, IDanmakuCard card, DanmakuInteractionController interactionController) : base(cardRuleData, card, interactionController)
        {
        }


        public override List<TargetableQueryResult> GetAnyValidTargetables(IDanmakuActivator danmakuActivator)
        {
            List<TargetableQueryResult> allPossibleTargetables = new ();
            List<IDanmakuTargetable> targetables = new ();
            
            var activatorPlayer = danmakuActivator as DanmakuPlayerModel;
            targetables.Add(activatorPlayer);
            
            allPossibleTargetables.Add(new TargetableQueryResult(TargetableTypeEnum.Player, targetables));

            return allPossibleTargetables;
        }

        public override bool CanPlayRule(IDanmakuActivator activator)
        {
            return activator is DanmakuPlayerModel;
        }

        public override bool CanExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            return activator is DanmakuPlayerModel activatorPlayer;

        }

        public override void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            if (CanExecuteRule(activator, targetables) == false)
            {
                return;
            }
            
            var playerModel = activator as DanmakuPlayerModel;
            
            InteractionController.BoardController.DrawCard(playerModel, CardRuleScriptableData.CardRuleValueWithIcons[0].Value);
            
        }
    }
}