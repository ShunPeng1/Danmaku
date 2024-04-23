using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using BNG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCardHandView : DanmakuCardHandBaseView
    {
        [Header("Snap Zone Settings")]
        
        [SerializeField] private SnapZone _snapZonePrefab;
        [SerializeField] private Transform _snapZoneStartingTransform;
        [SerializeField] private Vector3 _snapZoneOffset = new Vector3(0.3f, 0, 0);
        
        private List<SnapZone> _snapZones = new();
        
        
        [Header("Tween Settings")]
        [SerializeField] private float _addCardMoveDuration = 0.5f;
        [SerializeField] private float _addCardMoveDelay = 0.2f;
        [SerializeField] private Ease _tweenEase = Ease.Linear;

        
        

        public override void AddCard(DanmakuMainDeckCardBaseView cardView,IDanmakuCard card)
        {
            CardToView.Add(card, cardView);

            // Move card to snap zone
            var snapZone = CreateSnapZone();

            var vrCardView = (VRMainDeckCardView)cardView;
            vrCardView.TweenMove(snapZone.transform.position, snapZone.transform.rotation.eulerAngles, _addCardMoveDuration, _tweenEase,
                () =>
                {
                    snapZone.GrabGrabbable(cardView.GetComponent<Grabbable>());
                });
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

        public override void AllowCardPlay()
        {
            foreach (var snapZone in _snapZones)
            {
                //snapZone.CanDropItem = true;
                //snapZone.CanRemoveItem = true;
            }
        }

        public override void DisallowCardPlay()
        {
            foreach (var snapZoneInteractor in _snapZones)
            {
                //snapZoneInteractor.CanDropItem = false;
                //snapZoneInteractor.CanRemoveItem = false;
            }
        }

        private SnapZone CreateSnapZone(Grabbable startingItem = null)
        {
            var snapZone = Instantiate(_snapZonePrefab, _snapZoneStartingTransform);
            snapZone.transform.position = _snapZoneStartingTransform.position;
            snapZone.transform.localPosition += _snapZoneOffset * _snapZones.Count;
            snapZone.transform.rotation = _snapZoneStartingTransform.rotation;
            
            
            snapZone.CanDropItem = true;
            snapZone.CanRemoveItem = true;

            snapZone.StartingItem = startingItem;
            
            _snapZones.Add(snapZone);
            
            return snapZone;
        }
    }
}