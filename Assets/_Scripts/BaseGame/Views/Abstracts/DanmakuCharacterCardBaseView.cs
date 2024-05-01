using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public abstract class DanmakuCharacterCardBaseView : DanmakuCardBaseView
    {
        protected DanmakuCharacterCardModel CharacterCardModel;
        public abstract void SetCardModel(DanmakuCharacterCardModel characterCard);
    }
}