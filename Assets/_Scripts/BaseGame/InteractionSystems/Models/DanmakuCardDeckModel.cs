using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using Shun_Utilities;


namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuCardDeckModel : IDanmakuCardHolder
    {
        public ObservableList<IDanmakuCard> Cards { get; }
        
        public DanmakuCardDeckModel(List<IDanmakuCard> cards)
        {
            Cards = new ObservableList<IDanmakuCard>(cards);
        }

        public void AddCard(IDanmakuCard danmakuCard)
        {
            Cards.Add(danmakuCard);
        }

        public void AddCardAt(IDanmakuCard danmakuCard, int index)
        {
            Cards.Insert(index, danmakuCard);
        }

        public IDanmakuCard PopCardFront()
        {
            for (var index = 0; index < Cards.Count; index++)
            {
                var card = Cards[index];
                
                if (card != null)
                {
                    Cards.RemoveAt(index);
                    return card;
                }
            }
            
            return null;
        }

        public IDanmakuCard PopCardBack()
        {
            for (var index = Cards.Count - 1; index >= 0; index--)
            {
                var card = Cards[index];
                
                if (card != null)
                {
                    Cards.RemoveAt(index);
                    return card;
                }
            }
            
            return null;
        }

        public void RemoveCard(IDanmakuCard danmakuCard)
        {
            Cards.Remove(danmakuCard);
        }

        public void RemoveCardAt(int index)
        {
            Cards.RemoveAt(index);
        }

        public void MoveCard(IDanmakuCard danmakuCard, IDanmakuCardHolder targetHolder)
        {
            Cards.Remove(danmakuCard);
            targetHolder.AddCard(danmakuCard);
        }

        public void ShuffleHolder()
        {
            Cards.List.Shuffle();
        }
    }
}
    