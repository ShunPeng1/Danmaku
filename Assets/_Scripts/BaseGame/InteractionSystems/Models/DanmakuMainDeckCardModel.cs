using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;

using Shun_Utilities;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuMainDeckCardModel : IDanmakuCard
    {
        public DeckCardScriptableData DeckCardScriptableData { get; private set; }
        private readonly ObservableData<IDanmakuCardHolder> _cardHolder;
        private readonly List<DanmakuCardRuleBase> _cardRuleModels;
        
        public bool IsHidden { get; private set; }
        
        public DanmakuMainDeckCardModel(DeckCardScriptableData deckCardScriptableData, List<DanmakuCardRuleBase> cardRuleModels, IDanmakuCardHolder cardHolder )
        {
            DeckCardScriptableData = deckCardScriptableData;
            _cardRuleModels = cardRuleModels;
            _cardHolder = new ObservableData<IDanmakuCardHolder>(cardHolder);
        }
        
        public void InitializeCard()
        {
            
        }

        public void HideCard()
        {
            
        }
        
        public bool IsPlayable()
        {
            foreach (var rule in _cardRuleModels)
            {
                var activator = rule.GetAnyValidActivator();
                var listTargetables = rule.GetAnyValidTargetables(activator);

                // Check if there are any valid activators and targetables
                
                if (activator != null && listTargetables != null)
                {
                    return true;
                }
                
            }

            return false;
        }

        public List<RuleTargetablesQueryResult> GetPlayableRules()
        {
            var listRuleTargetables = new List<RuleTargetablesQueryResult>();
            foreach (var rule in _cardRuleModels)
            {
                var activator = rule.GetAnyValidActivator();
                var listTargetables = rule.GetAnyValidTargetables(activator);

                // Check if there are any valid activators and targetables
                
                if (activator != null && listTargetables != null)
                {
                    listRuleTargetables.Add(new RuleTargetablesQueryResult(rule, activator, listTargetables));
                }
            }

            return listRuleTargetables;
        }

        
        public void RevealCard()
        {
            
        }

        public void ExecuteCard(IDanmakuCardRule cardRule, IDanmakuActivator activators, List<IDanmakuTargetable> targetables)
        {
            cardRule.ExecuteRule(activators, targetables);
        }

        public void DiscardCard()
        {
            
        }

        public void DrawCard(DanmakuPlayerModel danmakuPlayerModel)
        {
            
        }

        public void SetCardHolder(IDanmakuCardHolder danmakuPlayerModel)
        {
            _cardHolder.Value = danmakuPlayerModel;
        }
        
        public DanmakuPlayerModel GetCardOwner()
        {
            return _cardHolder.Value.Owner;
        }

        public void ShowCard(DanmakuPlayerModel showToPlayerModel)
        {
            
        }

        public string PrintDebug()
        {
            var cardRuleNames = string.Join(", ", _cardRuleModels.Select(rule => rule.CardRuleScriptableData.CardRuleName));
            return DeckCardScriptableData.CardName + ": " + cardRuleNames;
        }
    }
}