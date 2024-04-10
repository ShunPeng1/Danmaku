using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockCardHandView : DanmakuCardHandBaseView
    {
        public override void DrawCard(IDanmakuCard card)
        {
            Debug.Log("Draw card: "+ ((DanmakuMainDeckCardModel)card).DeckCardScriptableData.CardName);
        }

        public override void DrawCards(List<IDanmakuCard> cards)
        {
            foreach (var card in cards)
            {
                DrawCard(card);
            }
        }
    }
}