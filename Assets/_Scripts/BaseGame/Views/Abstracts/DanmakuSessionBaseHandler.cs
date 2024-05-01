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
        [ShowInInspector, ReadOnly] public DanmakuCardSelectionBaseView CardSelectionView;
        [ShowInInspector, ReadOnly] public DanmakuPlayerBaseView PlayerView;
        
        protected DanmakuSession CurrentSession;

        protected void Awake()
        {
            PlayerView = transform.GetComponentInParent<DanmakuPlayerBaseView>();
            CardSelectionView = transform.GetComponentInChildren<DanmakuCardSelectionBaseView>();
        }

        private void Start()
        {
            CardSelectionView.HideSelection();
            
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
            CardSelectionView.ShowSelection();
            CardSelectionView.AddCardsToSelection(cardViews);
        }
    }
}