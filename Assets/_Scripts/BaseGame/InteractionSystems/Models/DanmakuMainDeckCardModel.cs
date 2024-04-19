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
        public ObservableData<IDanmakuCardHolder> CardHolder { get; private set; }
        public List<DanmakuCardRuleBase> CardRuleModels { get; private set; }
        
        public bool IsHidden { get; private set; }
        
        public DanmakuMainDeckCardModel(DeckCardScriptableData deckCardScriptableData, List<DanmakuCardRuleBase> cardRuleModels, IDanmakuCardHolder cardHolder )
        {
            DeckCardScriptableData = deckCardScriptableData;
            CardRuleModels = cardRuleModels;
            CardHolder = new ObservableData<IDanmakuCardHolder>(cardHolder);
        }
        
        public void InitializeCard()
        {
            
        }

        public void HideCard()
        {
            
        }

        public void RevealCard()
        {
            
        }

        public void ExecuteCard(IDanmakuCardRule cardRule, List<IDanmakuActivator> activators, List<IDanmakuTargetable> targetables)
        {
            cardRule.ExecuteRule(activators, targetables);
        }
        

        public void DiscardCard()
        {
            
        }

        public void DrawCard(DanmakuPlayerModel danmakuPlayerModel)
        {
            
        }

        public void SetCardOwner(DanmakuPlayerModel danmakuPlayerModel)
        {
            
        }

        public void ShowCard(DanmakuPlayerModel showToPlayerModel)
        {
            
        }

        public string PrintDebug()
        {
            var cardRuleNames = string.Join(", ", CardRuleModels.Select(rule => rule.CardRuleScriptableData.CardRuleName));
            return DeckCardScriptableData.CardName + ": " + cardRuleNames;
        }
    }
}