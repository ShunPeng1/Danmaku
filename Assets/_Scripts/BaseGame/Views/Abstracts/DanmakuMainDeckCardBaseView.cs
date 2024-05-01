using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuMainDeckCardBaseView : DanmakuCardBaseView
    {
        public DanmakuMainDeckCardModel CardModel { get; private set; }

        public void SetCardModel(DanmakuMainDeckCardModel cardModel)
        {
            CardModel = cardModel;
        }

        public abstract void CheckPlayable();
        public abstract void SetNotPlayable();
    }
}