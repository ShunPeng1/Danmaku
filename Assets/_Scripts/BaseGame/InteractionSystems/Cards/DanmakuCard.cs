using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;

namespace _Scripts.CoreGame.InteractionSystems.Cards
{
    public class DanmakuCard : IDanmakuCard
    {
        public DeckCardScriptableData DeckCardScriptableData { get; private set; }
        public IDanmakuCardHolder CardHolder { get; private set; }
        public List<IDanmakuCardRule> DanmakuCardRules { get; private set; }
        
        public bool IsHidden { get; private set; }
        
        public DanmakuCard(DeckCardScriptableData deckCardScriptableData)
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