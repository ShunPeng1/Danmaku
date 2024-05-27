using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using Shun_Utilities;


namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuCardHandModel : IDanmakuCardHolder
    {
        public ObservableArray<IDanmakuCard> Cards { get; } = new();

        public DanmakuPlayerModel Owner { get; private set; }
        
        public DanmakuCardHandModel(DanmakuPlayerModel owner)
        {
            Owner = owner;
        }

        public T GetFrontCard<T>() where T : IDanmakuCard
        {
            return (T) Cards.Items[0];
        }
        
        public void AddCard(IDanmakuCard danmakuCard)
        {
            if (Cards.TryAdd(danmakuCard))
            {
                danmakuCard.SetCardHolder(this);
            }
            
        }

        public void AddCardAt(IDanmakuCard danmakuCard, int index)
        {
            if (Cards.TryAddAt(index, danmakuCard))
            {
                danmakuCard.SetCardHolder(this);
            }
        }

        public IDanmakuCard PopCardFront()
        {
            for (var index = 0; index < Cards.Items.Length; index++)
            {
                var card = Cards.Items[index];
                
                if (card != null)
                {
                    Cards.TryRemoveAt(index);
                    return card;
                }
            }
            
            return null;
        }

        public IDanmakuCard PopCardBack()
        {
            for (var index = Cards.Items.Length - 1; index >= 0; index--)
            {
                var card = Cards.Items[index];
                
                if (card != null)
                {
                    Cards.TryRemoveAt(index);
                    return card;
                }
            }
            
            return null;
        }
        
        public void RemoveCard(IDanmakuCard danmakuCard)
        {
            Cards.TryRemove(danmakuCard);
        }

        public void RemoveCardAt(int index)
        {
            Cards.TryRemoveAt(index);
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
    