using System.Collections;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCardHandView : DanmakuCardHandBaseView
    {
        [SerializeField] Transform _cardHandTransform;
        
        [Header("Tween Settings")]
        [SerializeField] private float _addCardMoveDuration = 0.5f;
        [SerializeField] private float _addCardMoveDelay = 0.2f;
        [SerializeField] private Ease _tweenEase = Ease.Linear;
        
        
        
        public override void AddCard(DanmakuMainDeckCardBaseView cardView,IDanmakuCard card)
        {
            cardView.transform.DOMove(_cardHandTransform.position, _addCardMoveDuration).OnComplete(()=>
            {
                cardView.transform.SetParent(_cardHandTransform);
            });
            cardView.transform.DORotate(_cardHandTransform.rotation.eulerAngles, _addCardMoveDuration);
            CardToView.Add(card, cardView);
        }

        public override void AddCard(Dictionary<IDanmakuCard, DanmakuMainDeckCardBaseView> cardToView)
        {
            StartCoroutine(AddCardsWithDelay(cardToView));
        }

        private IEnumerator AddCardsWithDelay(Dictionary<IDanmakuCard, DanmakuMainDeckCardBaseView> cardToView)
        {
            foreach (var (card, view) in cardToView)
            {
                AddCard(view, card);
                yield return new WaitForSeconds(_addCardMoveDelay);
            }
        }
        
        public override void RemoveCard(IDanmakuCard card)
        {
            if (CardToView.ContainsKey(card))
            {
                var cardView = CardToView[card];
                CardToView.Remove(card);
            }
        }
        
        public override void RemoveCard(IDanmakuCard[] cards)
        {
            foreach (var card in cards)
            {
                RemoveCard(card);
                
            }
        }
    }
}