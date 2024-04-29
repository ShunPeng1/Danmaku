using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockCardHandView : DanmakuCardHandBaseView
    {

        public override void AddCard(DanmakuMainDeckCardBaseView cardView, IDanmakuCard card)
        {
            Debug.Log("Draw card: "+ ((DanmakuMainDeckCardModel)card).DeckCardData.CardName);
            CardToView.Add(card, cardView);
        }

        public override void AddCard(Dictionary<IDanmakuCard, DanmakuMainDeckCardBaseView> cardToView)
        {
            foreach (var (card,view) in cardToView)
            {
                AddCard(view,card);
            }
        }

        public override void RemoveCard(IDanmakuCard card)
        {
            Debug.Log("Remove card: "+ ((DanmakuMainDeckCardModel)card).DeckCardData.CardName);

            if (CardToView.ContainsKey(card))
            {
                var cardView = CardToView[card];
                CardToView.Remove(card);
            }
        }

        public override void RemoveCard(IDanmakuCard[] card)
        {
            foreach (var c in card)
            {
                RemoveCard(c);
            }
        }

        public override void AllowCardPlay()
        {
            throw new System.NotImplementedException();
        }

        public override void DisallowCardPlay()
        {
            throw new System.NotImplementedException();
        }
    }
}