using System.Collections.Generic;
using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.BaseGame.Views.Basics;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuCardSelectionBaseView : MonoBehaviour {
        public abstract void ShowSelection();
        public abstract void HideSelection();
        public abstract void AddCardsToSelection(List<DanmakuCardBaseView> characterCardViews);
    }
}