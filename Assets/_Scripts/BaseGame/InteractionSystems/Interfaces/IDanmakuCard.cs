using _Scripts.CoreGame.InteractionSystems;

namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCard
    {
        public void InitializeCard();

        public void HideCard();
        public void RevealCard();
        public void PlayCard(IDanmakuCardRule danmakuCardRule);
        public void DiscardCard();
        
        public void DrawCard(DanmakuPlayerModel danmakuPlayerModel);
        public void SetCardOwner(DanmakuPlayerModel danmakuPlayerModel);
        
        public void ShowCard(DanmakuPlayerModel showToPlayerModel);
        
        
    }
}