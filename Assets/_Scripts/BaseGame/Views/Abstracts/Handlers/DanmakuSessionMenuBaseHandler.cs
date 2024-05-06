using System.Collections.Generic;
using _Scripts.BaseGame.Views.Basics;
using _Scripts.CoreGame.InteractionSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuSessionMenuBaseHandler : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] public List<DanmakuCardPlayBaseView> CardPlayViews;
        [SerializeField] protected DanmakuCardPlayBaseView CardPlayViewPrefab;
        [SerializeField] protected Transform StartingTransform;
        [SerializeField] protected Vector3 Offset;
        
        protected readonly List<DanmakuSessionMenu> SessionMenus = new();
        
        public abstract void AddSessionMenu(DanmakuSessionMenu sessionMenu);
        public abstract void RemoveSessionMenu(DanmakuSessionMenu sessionMenu);
        
        protected DanmakuCardPlayBaseView CreateCardPlayView(DanmakuSessionChoice sessionChoice)
        {
            var cardPlayView = Instantiate(CardPlayViewPrefab, StartingTransform);

            var cardTransform = cardPlayView.transform;
            cardTransform.position = StartingTransform.position;
            cardTransform.localPosition += StartingTransform.position * SessionMenus.Count;
            cardTransform.rotation = StartingTransform.rotation;
            
            cardPlayView.SetSessionChoice(sessionChoice);
            CardPlayViews.Add(cardPlayView);
            return cardPlayView;
        }
        
        protected void RemoveCardPlayView(DanmakuCardPlayBaseView cardPlayView)
        {
            CardPlayViews.Remove(cardPlayView);
            Destroy(cardPlayView.gameObject);
        }
        
        protected void ClearCardPlayViews()
        {
            foreach (var cardPlayView in CardPlayViews)
            {
                Destroy(cardPlayView.gameObject);
            }
            CardPlayViews.Clear();
        }
    }
}