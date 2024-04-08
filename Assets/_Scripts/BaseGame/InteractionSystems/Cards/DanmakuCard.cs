using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems.Cards
{
    public class DanmakuCard : IDanmakuCard
    {
        public DanmakuPlayerModel CardOwner { get; private set; }
        public List<IDanmakuCardRule> DanmakuCardRules { get; private set; }
        
        public bool IsHidden { get; private set; }
        
        public DanmakuCard()
        {
            
        }
        
        public void InitializeCard()
        {
            
        }

        public void HideCard()
        {
            
        }

        public void RevealCard()
        {
            throw new System.NotImplementedException();
        }

        public void PlayCard(IDanmakuCardRule danmakuCardRule)
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

        public void SetCardOwner(DanmakuPlayerModel danmakuPlayerModel)
        {
            throw new System.NotImplementedException();
        }

        public void ShowCard(DanmakuPlayerModel showToPlayerModel)
        {
            throw new System.NotImplementedException();
        }
    }
}