namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCardHolder
    {
        public void AddCard(IDanmakuCard danmakuCard);
        public void RemoveCard(IDanmakuCard danmakuCard);
        public void MoveCard(IDanmakuCard danmakuCard, IDanmakuCardHolder targetHolder);
        public void ShuffleHolder();
        
    }
}