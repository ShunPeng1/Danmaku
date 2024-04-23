using _Scripts.CoreGame.InteractionSystems;

namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCardHolder
    {
        
        public DanmakuPlayerModel Owner { get;}
        public void AddCard(IDanmakuCard danmakuCard);
        public void AddCardAt(IDanmakuCard danmakuCard, int index);
        
        public IDanmakuCard PopCardFront();
        public IDanmakuCard PopCardBack();
        public void RemoveCard(IDanmakuCard danmakuCard);
        
        public void RemoveCardAt(int index);
        
        public void MoveCard(IDanmakuCard danmakuCard, IDanmakuCardHolder targetHolder);
        public void ShuffleHolder();
        
    }
}