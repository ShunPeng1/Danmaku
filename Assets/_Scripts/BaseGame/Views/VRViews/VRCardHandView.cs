using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCardHandView : DanmakuCardHandBaseView
    {
        [Header("Socket")]
        [SerializeField] private XRSocketInteractor _socketInteractorPrefab;
        [SerializeField] private Transform _socketStartingTransform;
        [SerializeField] private Vector3 _socketOffset;
        [SerializeField] private Transform _cardHandTransform;
        private List<XRSocketInteractor> _socketInteractors = new();
        
        
        [Header("Tween Settings")]
        [SerializeField] private float _addCardMoveDuration = 0.5f;
        [SerializeField] private float _addCardMoveDelay = 0.2f;
        [SerializeField] private Ease _tweenEase = Ease.Linear;

        private void Awake()
        {
            CreateSocketInteractors();
        }

        public void InitializeSocketInteractors(int socketCount)
        {
            _socketInteractors = new List<XRSocketInteractor>();
            for (int i = 0; i < socketCount; i++)
            {
                var socketInteractor = Instantiate(_socketInteractorPrefab, _cardHandTransform);
                _socketInteractors.Add(socketInteractor);
            }
        }

        public override void AddCard(DanmakuMainDeckCardBaseView cardView,IDanmakuCard card)
        {
             var socket = CreateSocketInteractors();
            
            cardView.transform.DOMove(socket.transform.position, _addCardMoveDuration).OnComplete(()=>
            {
                cardView.transform.SetParent(_cardHandTransform);
                socket.socketActive = true;
            });
            cardView.transform.DORotate(socket.transform.rotation.eulerAngles, _addCardMoveDuration);
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
        
        private XRSocketInteractor CreateSocketInteractors()
        {
            var newSocketInteractors = Instantiate(_socketInteractorPrefab, _socketStartingTransform);
            newSocketInteractors.transform.position = _socketStartingTransform.position;
            newSocketInteractors.transform.localPosition += _socketOffset * _socketInteractors.Count;
            newSocketInteractors.transform.rotation = _socketStartingTransform.rotation;
            newSocketInteractors.socketActive = false;
            
            _socketInteractors.Add(newSocketInteractors);
            
            return newSocketInteractors;
        }
    }
}