using System.Collections.Generic;
using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.BaseGame.Views.Basics;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuCardShowcaseBaseView : MonoBehaviour 
    {
        protected DanmakuSessionBaseHandler SessionHandler;
        private void Awake()
        {
            SessionHandler = transform.GetComponentInParent<DanmakuSessionBaseHandler>();
        }
        
        public abstract void Show();
        public abstract void Hide();
        public abstract void AddCardsToShowcase(List<DanmakuCardBaseView> characterCardViews);

        public abstract List<DanmakuCardBaseView> ClearCardsFromSelection();
    }
}