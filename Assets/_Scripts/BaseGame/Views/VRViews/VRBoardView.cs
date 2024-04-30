using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Positions;
using _Scripts.CoreGame.InteractionSystems;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRBoardView : DanmakuBoardBaseView
    {
        [Header("Prefabs")]
        [SerializeField] private DanmakuMainDeckCardBaseView _mainDeckCardPrefab;
        [SerializeField] private DanmakuCharacterCardBaseView _characterCardPrefab;
        [SerializeField] private DanmakuPlayerBaseView _playerPrefab;
        
        [Header("Transforms")]
        [SerializeField] private PlayerStandingPositionMap _playerStandingPositionMap;
        [SerializeField] private Transform _topDrawMainDeck;
        [SerializeField] private Transform _topDiscardMainDeck;
        [SerializeField] private Transform _topDrawCharacterDeck;
        [SerializeField] private Transform _topDiscardCharacterDeck;
        

        protected override void InitializeInherit()
        {
            
        }

        public override Dictionary<DanmakuPlayerModel, DanmakuPlayerBaseView> CreatePlayerViews(List<DanmakuPlayerModel> playerModels)
        {
            var playerModelToViews = new Dictionary<DanmakuPlayerModel, DanmakuPlayerBaseView>();
            foreach (var playerModel in playerModels)
            {
                var standingPosition = _playerStandingPositionMap.GetPlayerPosition(playerModels.Count, playerModel.PlayerId);
                var playerView = Instantiate(_playerPrefab, standingPosition.position, standingPosition.rotation, transform);
                playerView.name = $"PlayerView_{playerModel.PlayerId}";
                playerModelToViews.Add(playerModel, playerView);
                
                playerView.InitializeView();
            }
            
            return playerModelToViews;
        }

        public override void DrawCardFromMainDeck(DanmakuPlayerModel playerModel, DanmakuMainDeckCardModel card)
        {
            var handView = InteractionViewRepo.GetPlayerView(playerModel).CardHandView;

            var cardView = Instantiate(_mainDeckCardPrefab, _topDrawMainDeck);
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
            var cardView = Instantiate(_mainDeckCardPrefab, _topDrawMainDeck);
            cardView.SetCardModel((DanmakuMainDeckCardModel)card);

            cardView.transform.DOMove(_topDiscardMainDeck.position, 0.5f).OnComplete(() => { Destroy(cardView.gameObject); });
        }

        public override void DiscardCardsToDiscardDeck(DanmakuPlayerModel playerModel, List<DanmakuMainDeckCardModel> cards)
        {
            foreach (var card in cards)
            {
                DiscardCardToDiscardDeck(playerModel, card);
            }
        }

        public override void SetupMainDeck(DanmakuCardDeckModel mainDeckModel)
        {
            Debug.Log("Setting up main deck "+mainDeckModel.Cards.Count);
        }

        public override void SetupIncidentDeck(DanmakuCardDeckModel incidentDeckModel)
        {
            Debug.Log("Setting up incident deck "+incidentDeckModel.Cards.Count);
        }

        public override void SetupCharacterDeck(DanmakuCardDeckModel characterDeckModel)
        {
            Debug.Log("Setting up character deck "+characterDeckModel.Cards.Count);
        }

        public override void DrawCharacterCardsForSelection(DanmakuPlayerModel player, List<DanmakuCharacterCardModel> characterCards)
        {
            var playerView = InteractionViewRepo.GetPlayerView(player);
            List<DanmakuCharacterCardBaseView> characterCardViews = new List<DanmakuCharacterCardBaseView>(); 
            foreach (var characterCard in characterCards)
            {
                DanmakuCharacterCardBaseView cardView = Instantiate(_characterCardPrefab, _topDrawCharacterDeck);
                cardView.SetCardModel(characterCard);
                
                characterCardViews.Add(cardView);
            }
            
            playerView.SetupCharacterSelection(characterCardViews);
            
        }
    }
}