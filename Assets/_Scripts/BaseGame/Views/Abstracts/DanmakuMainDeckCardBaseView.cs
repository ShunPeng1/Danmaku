using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuMainDeckCardBaseView : MonoBehaviour
    {
        public DanmakuMainDeckCardModel CardModel { get; private set; }

        public void SetCardModel(DanmakuMainDeckCardModel cardModel)
        {
            CardModel = cardModel;
        }
    }
}