using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems.Attributes;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using Shun_Utilities;

namespace _Scripts.CoreGame.InteractionSystems
{
    
    [DanmakuTargetableClass]
    public class DanmakuCharacterCardModel : IDanmakuCard
    {
        public CharacterCardScriptableData CharacterCardData { get; private set; }
        public readonly IDanmakuCharacter Character;
        
        private readonly ObservableData<IDanmakuCardHolder> _cardHolder;
        private readonly List<DanmakuCardRuleBase> _cardRuleModels;
        
        public DanmakuCharacterCardModel(CharacterCardScriptableData characterCardData, List<DanmakuCardRuleBase> cardRuleModels, IDanmakuCardHolder cardHolder, IDanmakuCharacter character)
        {
            CharacterCardData = characterCardData;
            _cardRuleModels = cardRuleModels;
            Character = character;
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
            return "Character Card";
        }
    }
}