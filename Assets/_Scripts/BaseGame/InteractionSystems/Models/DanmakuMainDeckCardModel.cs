using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;

using Shun_Utilities;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuMainDeckCardModel : IDanmakuCard
    {
        public DeckCardScriptableData DeckCardScriptableData { get; private set; }
        public ObservableData<IDanmakuCardHolder> CardHolder { get; private set; }
        public List<IDanmakuCardRule> DanmakuCardRules { get; private set; }
        
        public bool IsHidden { get; private set; }
        
        public DanmakuMainDeckCardModel(DeckCardScriptableData deckCardScriptableData)
        {
            DeckCardScriptableData = deckCardScriptableData;
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

        public void PlayCard(IDanmakuCardRule danmakuCardRule)
        {
            
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
    }
}