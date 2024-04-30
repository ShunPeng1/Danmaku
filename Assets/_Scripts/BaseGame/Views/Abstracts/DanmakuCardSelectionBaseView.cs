using System.Collections.Generic;
using _Scripts.BaseGame.Views.Basics;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuCardSelectionBaseView : MonoBehaviour {
        public abstract void ShowCharacterCardsSelection(List<DanmakuCharacterCardBaseView> characterCardViews);
    }
}