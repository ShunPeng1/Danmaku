using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRBoardView : DanmakuBoardBaseView
    {
        [Header("Prefabs")]
        [SerializeField] private DanmakuMainDeckCardBaseView _mainDeckCardPrefab;

        [Header("Transforms")]
        [SerializeField] private Transform _mainDeckHolder;
        [SerializeField] private Transform _discardDeckHolder;


        protected override void InitializeInherit()
        {
            
        }

        public override void DrawCardFromMainDeck(DanmakuPlayerModel playerModel, DanmakuMainDeckCardModel card)
        {
            var handView = InteractionViewRepo.SetupPlayerView.GetPlayerView(playerModel).CardHandView;

            var cardView = Instantiate(_mainDeckCardPrefab, _mainDeckHolder);
            cardView.SetCardModel(card);

            handView.AddCard(cardView, card);
            
        }

        public override void DrawCardFromMainDeck(DanmakuPlayerModel playerModel, List<DanmakuMainDeckCardModel> cards)
        {
            foreach (var card in cards)
            {
                DrawCardFromMainDeck(playerModel, card);
            }
            
        }

        public override void DiscardCardToDiscardDeck(DanmakuPlayerModel playerModel, DanmakuMainDeckCardModel card)
        {
            var cardView = Instantiate(_mainDeckCardPrefab, _mainDeckHolder);
            cardView.SetCardModel((DanmakuMainDeckCardModel)card);

            cardView.transform.DOMove(_discardDeckHolder.position, 0.5f).OnComplete(() => { Destroy(cardView.gameObject); });
        }

        public override void DiscardCardsToDiscardDeck(DanmakuPlayerModel playerModel, List<DanmakuMainDeckCardModel> cards)
        {
            foreach (var card in cards)
            {
                DiscardCardToDiscardDeck(playerModel, card);
            }
        }
    }
}