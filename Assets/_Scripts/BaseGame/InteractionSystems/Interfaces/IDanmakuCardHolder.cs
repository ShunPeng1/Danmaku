namespace _Scripts.BaseGame.InteractionSystems.Interfaces
{
    public interface IDanmakuCardHolder
    {
        public void AddCard(IDanmakuBaseCard danmakuBaseCard);
        public void RemoveCard(IDanmakuBaseCard danmakuBaseCard);
        public void DiscardCard(IDanmakuBaseCard danmakuBaseCard);
        public void PlayCard(IDanmakuBaseCard danmakuBaseCard);
        public void ShuffleHolder();
        
    }
}