using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuCardHandBaseView : MonoBehaviour
    {
        public Dictionary<IDanmakuCard, DanmakuMainDeckCardBaseView> CardToView { get; private set; } = new();
        
        
        public abstract void AddCard(DanmakuMainDeckCardBaseView cardView,IDanmakuCard card);
        public abstract void AddCard(Dictionary<IDanmakuCard,DanmakuMainDeckCardBaseView> cardToView);

        public abstract void RemoveCard(IDanmakuCard card);
        public abstract void RemoveCard(IDanmakuCard[] card);


    }
}