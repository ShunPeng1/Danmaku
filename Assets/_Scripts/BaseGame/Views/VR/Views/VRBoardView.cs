using System;
using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Abstracts;
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
        
        [Header("Tween Move")]
        [SerializeField] private float _moveDuration = 0.5f;
        [SerializeField] private Ease _moveEase = Ease.InOutCubic;
        
        

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
                
                playerView.InitializeView(playerModel);
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
            var handView = InteractionViewRepo.GetPlayerView(playerModel).CardHandView;
            Dictionary<IDanmakuCard, DanmakuMainDeckCardBaseView> cardToView = new();
            foreach (var card in cards)
            {
                var cardView = Instantiate(_mainDeckCardPrefab, _topDrawMainDeck);
                cardView.SetCardModel(card);
                cardToView.Add(card, cardView);
            }
            
            handView.AddCard(cardToView);
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
            List<DanmakuCardBaseView> characterCardViews = new (); 
            foreach (var characterCard in characterCards)
            {
                DanmakuCharacterCardBaseView cardView = Instantiate(_characterCardPrefab, _topDrawCharacterDeck);
                cardView.SetCardModel(characterCard);
                
                characterCardViews.Add(cardView);
            }
            
            playerView.SessionHandler.AddCardsToSelection(characterCardViews);
        }

        public override void DiscardCharacterCardForSelection(DanmakuPlayerModel player)
        {
            var playerView = InteractionViewRepo.GetPlayerView(player);
            var cardViews = playerView.SessionHandler.RemoveCardsFromSelection();
            
            foreach (var cardView in cardViews)
            {
                cardView.transform.DOMove(_topDiscardCharacterDeck.position, _moveDuration).SetEase(_moveEase).OnComplete(
                    () =>
                    {
                        Destroy(cardView.gameObject); 
                        
                    });
                
            }
            
            
        }

        public override void AddSessionToPlayer(DanmakuSession session)
        {
            foreach (var player in session.PlayingPlayerModel)
            {
                var playerView = InteractionViewRepo.GetPlayerView((DanmakuPlayerModel)player);
                playerView.SessionHandler.SetCurrentSession(session);
            }
        }

        public override void RemoveSessionFromPlayer(DanmakuSession session)
        {
            foreach (var player in session.PlayingPlayerModel)
            {
                var playerView = InteractionViewRepo.GetPlayerView((DanmakuPlayerModel)player);
                playerView.SessionHandler.UnsetCurrentSession();
            }
        }
        
    }
}