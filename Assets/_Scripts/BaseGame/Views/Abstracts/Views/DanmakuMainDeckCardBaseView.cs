using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuMainDeckCardBaseView : DanmakuCardBaseView
    {
        
        public override void SetCardModel(IDanmakuCard cardModel)
        {
            if (cardModel is DanmakuMainDeckCardModel mainDeckCardModel)
            {
                CardModel = mainDeckCardModel;
            }
            else
            {
                Debug.LogError("Wrong card model type");
            }
        }
        

        public abstract void CheckPlayable();
        public abstract void SetNotPlayable();
    }
}