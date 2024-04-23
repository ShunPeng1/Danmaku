using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;

namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCard
    {
        public void InitializeCard();

        public void HideCard();
        
        public bool IsPlayable();
        public void RevealCard();
        public void ExecuteCard(IDanmakuCardRule cardRule, IDanmakuActivator activator, List<IDanmakuTargetable> targetables);
        public void DiscardCard();
        
        public void DrawCard(DanmakuPlayerModel danmakuPlayerModel);
        public void SetCardHolder(IDanmakuCardHolder danmakuPlayerModel);
        
        public DanmakuPlayerModel GetCardOwner();
        public void ShowCard(DanmakuPlayerModel showToPlayerModel);
        
        public string PrintDebug();
        
        
    }
}