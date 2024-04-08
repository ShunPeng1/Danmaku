using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using Shun_Utilities;


namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuCardDeckModel : IDanmakuCardHolder
    {
        public ObservableArray<IDanmakuCard> Cards { get; } = new();

        public void AddCard(IDanmakuCard danmakuCard)
        {
            Cards.TryAdd(danmakuCard);
        }

        public void RemoveCard(IDanmakuCard danmakuCard)
        {
            Cards.TryRemove(danmakuCard);
        }

        public void MoveCard(IDanmakuCard danmakuCard, IDanmakuCardHolder targetHolder)
        {
            Cards.TryRemove(danmakuCard);
            targetHolder.AddCard(danmakuCard);
        }

        public void ShuffleHolder()
        {
            Cards.Items.Shuffle();
        }
    }
}
    