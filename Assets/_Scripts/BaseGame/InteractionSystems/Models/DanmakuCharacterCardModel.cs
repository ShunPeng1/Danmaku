using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using Shun_Utilities;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuCharacterCardModel : IDanmakuCard
    {
        public CharacterCardScriptableData CharacterCardData { get; private set; }
        private readonly ObservableData<IDanmakuCardHolder> _cardHolder;
        private readonly List<DanmakuCardRuleBase> _cardRuleModels;
        
        public DanmakuCharacterCardModel(CharacterCardScriptableData characterCardData, List<DanmakuCardRuleBase> cardRuleModels, IDanmakuCardHolder cardHolder )
        {
            CharacterCardData = characterCardData;
            _cardRuleModels = cardRuleModels;
            _cardHolder = new ObservableData<IDanmakuCardHolder>(cardHolder);
        }
        
        public void InitializeCard()
        {
            throw new System.NotImplementedException();
        }

        public void HideCard()
        {
            throw new System.NotImplementedException();
        }

        public bool IsPlayable()
        {
            throw new System.NotImplementedException();
        }

        public List<RuleTargetablesQueryResult> GetPlayableRules()
        {
            throw new System.NotImplementedException();
        }

        public void RevealCard()
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteCard(IDanmakuCardRule cardRule, IDanmakuActivator activator, List<IDanmakuTargetable> targetables)
        {
            throw new System.NotImplementedException();
        }

        public void DiscardCard()
        {
            throw new System.NotImplementedException();
        }

        public void DrawCard(DanmakuPlayerModel danmakuPlayerModel)
        {
            throw new System.NotImplementedException();
        }

        public void SetCardHolder(IDanmakuCardHolder danmakuPlayerModel)
        {
            throw new System.NotImplementedException();
        }

        public DanmakuPlayerModel GetCardOwner()
        {
            throw new System.NotImplementedException();
        }

        public void ShowCard(DanmakuPlayerModel showToPlayerModel)
        {
            throw new System.NotImplementedException();
        }

        public string PrintDebug()
        {
            throw new System.NotImplementedException();
        }
    }
}