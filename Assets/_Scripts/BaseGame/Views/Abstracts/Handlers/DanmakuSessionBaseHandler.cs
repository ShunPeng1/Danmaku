using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.BaseGame.Views.Basics;
using _Scripts.CoreGame.InteractionSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public class DanmakuSessionBaseHandler : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] public DanmakuPlayerBaseView PlayerView;
        [ShowInInspector, ReadOnly] public DanmakuCardShowcaseBaseView CardShowcaseView;
        [ShowInInspector, ReadOnly] public DanmakuSessionMenuBaseHandler MenuHandler;
        
        protected DanmakuSession CurrentSession;

        protected void Awake()
        {
            PlayerView = transform.GetComponentInParent<DanmakuPlayerBaseView>();
            CardShowcaseView = transform.GetComponentInChildren<DanmakuCardShowcaseBaseView>();
            MenuHandler = transform.GetComponentInChildren<DanmakuSessionMenuBaseHandler>();
        }

        private void Start()
        {
            CardShowcaseView.Hide();
            
        }

        public virtual void SetCurrentSession(DanmakuSession session)
        {
            CurrentSession = session;
        }
        
        public virtual void UnsetCurrentSession()
        {
            CurrentSession = null;
        }

        public virtual void AddCardsToSelection(List<DanmakuCardBaseView> cardViews)
        {
            CardShowcaseView.Show();
            CardShowcaseView.AddCardsToShowcase(cardViews);
        }
        
        public virtual List<DanmakuCardBaseView> RemoveCardsFromSelection()
        {
            List<DanmakuCardBaseView> cardViews = CardShowcaseView.ClearCardsFromSelection();
            CardShowcaseView.Hide();
            return cardViews;
        }
        
    }
}