using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;

namespace _Scripts.CoreGame.InteractionSystems.CardRules
{
    public class Action1UpRule : DanmakuCardRuleBase
    {
        public Action1UpRule(CardRuleScriptableData cardRuleData, IDanmakuCard card, DanmakuInteractionController interactionController) : base(cardRuleData, card, interactionController)
        {
        }
        
        public override void InitializeCard()
        {
            throw new System.NotImplementedException();
        }

        public override List<List<IDanmakuTargetable>> GetAnyValidTargetables()
        {
            List<List<IDanmakuTargetable>> allPossibleTargetables = new ();
            foreach (var playerModel in InteractionController.PlayerGroupModel.Players)
            {
                List<IDanmakuTargetable> targetables = new ();

                if (playerModel.IsAlive && !playerModel.Life.IsGreaterOrEqualToMax())
                {
                    targetables.Add(playerModel);
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
            
            return targetables[0] is DanmakuPlayerModel playerModel && playerModel.IsAlive && !playerModel.Life.IsGreaterOrEqualToMax();
        }

        public override void ExecuteRule(IDanmakuActivator activator, List<IDanmakuTargetable> targetables = null)
        {
            if (targetables is not { Count: 1 } || CardRuleScriptableData.CardRuleValueWithIcons.Length != 1)
            {
                return;
            }
            
            var healAmount = CardRuleScriptableData.CardRuleValueWithIcons[0].Value;
            
            if (targetables[0] is DanmakuPlayerModel playerModel)
            {
                playerModel.Life.Increase(healAmount);
            }
        }
    }
}