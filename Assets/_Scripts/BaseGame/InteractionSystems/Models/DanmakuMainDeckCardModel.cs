using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using Shun_Utilities;

namespace _Scripts.CoreGame.InteractionSystems
{
    [DanmakuTargetableClass]
    public class DanmakuMainDeckCardModel : IDanmakuCard
    {
        public DeckCardScriptableData DeckCardData { get; private set; }
        private readonly ObservableData<IDanmakuCardHolder> _cardHolder;
        private readonly List<DanmakuCardRuleBase> _cardRuleModels;
        
        public Action<IDanmakuCardRule, IDanmakuActivator, List<IDanmakuTargetable>> OnCardExecuted { get; set; }
        
        public DanmakuMainDeckCardModel(DeckCardScriptableData deckCardData, List<DanmakuCardRuleBase> cardRuleModels, IDanmakuCardHolder cardHolder )
        {
            DeckCardData = deckCardData;
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
            OnCardExecuted?.Invoke(cardRule, activators, targetables);
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
            return DeckCardData.CardName + ": " + cardRuleNames;
        }
    }
}