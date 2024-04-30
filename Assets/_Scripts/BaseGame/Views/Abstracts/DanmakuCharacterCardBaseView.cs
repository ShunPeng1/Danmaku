using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public abstract class DanmakuCharacterCardBaseView : MonoBehaviour
    {
        protected DanmakuCharacterCardModel CharacterCardModel;
        public abstract void SetCardModel(DanmakuCharacterCardModel characterCard);
    }
}