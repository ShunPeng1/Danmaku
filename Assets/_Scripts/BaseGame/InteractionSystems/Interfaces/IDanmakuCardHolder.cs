namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCardHolder
    {
        public void AddCard(IDanmakuCard danmakuCard);
        public void AddCardAt(IDanmakuCard danmakuCard, int index);

        public void RemoveCard(IDanmakuCard danmakuCard);
        
        public void RemoveCardAt(int index);
        
        public void MoveCard(IDanmakuCard danmakuCard, IDanmakuCardHolder targetHolder);
        public void ShuffleHolder();
        
    }
}