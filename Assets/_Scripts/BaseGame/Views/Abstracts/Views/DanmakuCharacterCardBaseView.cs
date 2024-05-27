using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public abstract class DanmakuCharacterCardBaseView : DanmakuCardBaseView
    {
        
        public override void SetCardModel(IDanmakuCard cardModel)
        {
            if (cardModel is DanmakuCharacterCardModel characterCardModel)
            {
                CardModel = characterCardModel;
            }
            else
            {
                Debug.LogError("Wrong card model type");
            }
        }

    }
}