using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreGame.InteractionSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRSessionChoiceUICoordinator : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] public List<DanmakuCardPlayBaseView> CardPlayViews;
        [SerializeField] protected DanmakuCardPlayBaseView CardPlayViewPrefab;
        [SerializeField] protected Transform StartingTransform;
        [SerializeField] protected Vector3 Offset;
        
        public void CreateView(DanmakuSessionChoice sessionChoice)
        {
            
                    
            switch (sessionChoice.TargetType)
            {
                case var type when type == typeof(DanmakuCharacterCardModel):
                    // Handle DanmakuCharacterCardModel case
                    var characterCardPlayView = CreateCardPlayView(sessionChoice);
                    characterCardPlayView.SetSessionChoice(sessionChoice);
                    break;
                    
                case var type when type == typeof(DanmakuMainDeckCardModel):
                    // Handle DanmakuMainDeckCardModel case
                    var mainDeckCardPlayView = CreateCardPlayView(sessionChoice);
                    mainDeckCardPlayView.SetSessionChoice(sessionChoice);
                            
                    break;
                case var type when type == typeof(DanmakuPlayerModel):
                    // Handle DanmakuPlayerModel case
                    Debug.Log("Create Player Model Choice");
                            
                    break;
                // Add more cases as needed
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void RemoveView(DanmakuSessionChoice sessionChoice)
        {
            var cardPlayView = CardPlayViews.FirstOrDefault(view => view.SessionChoice == sessionChoice);
            if (cardPlayView != null)
            {
                RemoveCardPlayView(cardPlayView);
            }

        }

        private DanmakuCardPlayBaseView CreateCardPlayView(DanmakuSessionChoice sessionChoice)
        {
            var cardPlayView = Instantiate(CardPlayViewPrefab, StartingTransform);

            var cardTransform = cardPlayView.transform;
            cardTransform.position = StartingTransform.position;
            cardTransform.localPosition += StartingTransform.position * CardPlayViews.Count;
            cardTransform.rotation = StartingTransform.rotation;
            
            cardPlayView.SetSessionChoice(sessionChoice);
            CardPlayViews.Add(cardPlayView);
            return cardPlayView;
        }

        private void RemoveCardPlayView(DanmakuCardPlayBaseView cardPlayView)
        {
            CardPlayViews.Remove(cardPlayView);
            Destroy(cardPlayView.gameObject);
        }
        
        private void ClearCardPlayViews()
        {
            foreach (var cardPlayView in CardPlayViews)
            {
                Destroy(cardPlayView.gameObject);
            }
            CardPlayViews.Clear();
        }

    }
}