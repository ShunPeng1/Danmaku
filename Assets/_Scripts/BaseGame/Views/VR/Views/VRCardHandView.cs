using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Positions;
using BNG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCardHandView : DanmakuCardHandBaseView
    {
        [Header("Snap Zone Settings")]
        [SerializeField] private SnapZoneCoordinator _snapZoneCoordinator;

        
        [Header("Tween Settings")]
        [SerializeField] private float _addCardMoveDuration = 0.5f;
        [SerializeField] private float _addCardMoveDelay = 0.2f;
        [SerializeField] private Ease _tweenEase = Ease.Linear;
        private int _movingCardCount = 0;
        
        public override void AddCard(DanmakuMainDeckCardBaseView cardView,IDanmakuCard card)
        {
            CardToView.Add(card, cardView);
            SetCardToSnapZone(cardView,card);
        }

        public override void AddCard(Dictionary<IDanmakuCard, DanmakuMainDeckCardBaseView> cardToView)
        {
            foreach (var (card, cardView) in cardToView)
            {
                CardToView.Add(card, cardView);
            }
            
            StartCoroutine(AddCardsWithDelay(cardToView));
            
        }

        private IEnumerator AddCardsWithDelay(Dictionary<IDanmakuCard, DanmakuMainDeckCardBaseView> cardToView)
        {
            foreach (var (card, view) in cardToView)
            {
                SetCardToSnapZone(view, card);
                yield return new WaitForSeconds(_addCardMoveDelay);
            }
        }
        
        
        private void SetCardToSnapZone(DanmakuMainDeckCardBaseView cardView,IDanmakuCard card)
        {
            // Move card to snap zone
            var emptySnapZoneIndex = _snapZoneCoordinator.GetEmptySnapZoneIndex();
            
            var snapZone = _snapZoneCoordinator.GetEmptySnapZone(_movingCardCount + emptySnapZoneIndex);

            var vrCardView = (VRMainDeckCardView)cardView;
            
            _movingCardCount++;
            vrCardView.TweenMove(snapZone.transform.position, snapZone.transform.rotation.eulerAngles, _addCardMoveDuration, _tweenEase,
                () =>
                {
                    snapZone.GrabGrabbable(cardView.GetComponent<Grabbable>());
                    _movingCardCount--;
                });

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

        public override void AllowCardPlay()
        {
            foreach (var (card, view) in CardToView)
            {
                view.CheckPlayable();
            }
            
        }

        public override void DisallowCardPlay()
        {
            foreach (var (card, view) in CardToView)
            {
                view.SetNotPlayable();
            }
            
        }

    }
}