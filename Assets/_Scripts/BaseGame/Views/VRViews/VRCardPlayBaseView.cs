using System;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.BaseGame.Views.Basics.BNGExtension;
using _Scripts.CoreGame.InteractionSystems;
using BNG;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCardPlayBaseView : DanmakuCardPlayBaseView
    {
        [SerializeField] private SnapZone _playCardSnapZone;
        [SerializeField] private PlayCardSnapZoneFilter _playCardSnapZoneFilter;
        private DanmakuCardBaseView _playingCard;
        
        private Func<Grabbable, bool> _grabbableFilter;


        protected override void Awake()
        {
            base.Awake();
            
            _playCardSnapZone.OnSnapEvent.AddListener(SetPlayingCard);
            _playCardSnapZone.OnDetachEvent.AddListener(UnsetPlayingCard);
            _playCardSnapZoneFilter.SetOwner(SessionHandler.PlayerView.PlayerModel);
        }
        
        public override void SetSessionChoice(DanmakuSessionChoice sessionChoice)
        {
            base.SetSessionChoice(sessionChoice);

            _grabbableFilter = ConvertFilter(sessionChoice.CardFilter);
            _playCardSnapZoneFilter.AddMustIncludeFilter(_grabbableFilter);
        }
        
        public override void UnsetSessionChoice()
        {
            base.UnsetSessionChoice();
            
            _playCardSnapZoneFilter.RemoveMustIncludeFilter(_grabbableFilter);
        }
        
        private Func<Grabbable, bool> ConvertFilter(Func<IDanmakuTargetable, bool> cardBaseViewFilter)
        {
            return (grabbable) =>
            {
                var cardView = grabbable.GetComponent<DanmakuCardBaseView>();
                return cardBaseViewFilter(cardView.CardModel);
            };
        }
        
        public void SetPlayingCard(Grabbable grabbable)
        {
            _playingCard = grabbable.GetComponent<DanmakuCardBaseView>();
            
            if (SessionChoice != null)
            {
                SessionChoice.SelectTarget(_playingCard.CardModel);
            }
        }
        
        public void UnsetPlayingCard(Grabbable grabbable)
        {
            _playingCard = null;
            
            if (SessionChoice != null)
            {
                SessionChoice.DeselectTarget();
            }
        }
        
        
        
    }
}