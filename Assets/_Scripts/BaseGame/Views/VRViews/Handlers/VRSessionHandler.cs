using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.BaseGame.Views.Basics.BNGExtension;
using _Scripts.CoreGame.InteractionSystems;
using BNG;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRSessionHandler : DanmakuSessionBaseHandler
    {
        [SerializeField] private TargetSelectionCanvas _targetSelectionCanvas;
        [SerializeField] private SnapZone _playCardSnapZone;
        [SerializeField] private PlayCardSnapZoneFilter _playCardSnapZoneFilter;
        private VRMainDeckCardView _playingCard;
        private DanmakuSession _currentSession;
        private Func<Grabbable, bool> _grabbableFilter;
        
        private new void Awake()
        {
            base.Awake();
            
            _playCardSnapZone.OnSnapEvent.AddListener(SetPlayingCard);
            _playCardSnapZone.OnDetachEvent.AddListener(UnsetPlayingCard);
            _playCardSnapZoneFilter.SetOwner(PlayerView.PlayerModel);
        }
        
        public override void SetCurrentSession(DanmakuSession session)
        {
            if (_currentSession != null)
            {
                UnsetCurrentSession();
            }
            
            _currentSession = session;
            _grabbableFilter = ConvertFilter(_currentSession.CardFilter);
            _playCardSnapZoneFilter.AddMustIncludeFilter(_grabbableFilter);
        }
        
        public override void UnsetCurrentSession()
        {
            _playCardSnapZoneFilter.RemoveMustIncludeFilter(_grabbableFilter);
            _currentSession = null;
        }

        private Func<Grabbable, bool> ConvertFilter(Func<DanmakuCardBaseView, bool> cardBaseViewFilter)
        {
            return (grabbable) =>
            {
                var cardView = grabbable.GetComponent<DanmakuCardBaseView>();
                return cardBaseViewFilter(cardView);
            };
        }
        
        
        public void SetPlayingCard(Grabbable grabbable)
        {
            _playingCard = grabbable.GetComponent<VRMainDeckCardView>();
            
            
            
            var playableRules = _playingCard.CardModel.GetPlayableRules();
            
            foreach (var rule in playableRules)
            {
                var targetables = rule.Targetables;
                
                foreach (var targetable in targetables)
                {
                    var targetableType = targetable.TargetableType;
                    var targetablesList = targetable.Targetables;
                    
                    foreach (var target in targetablesList)
                    {
                        // Create a target selection view for each targetable
                        // and set the targetable to the target selection view
                        
                        Debug.Log("Targetable " + target);
                        
                    }
                }
            }
        }
        
        private void UnsetPlayingCard(Grabbable arg0)
        {
            _playingCard = null;
            
            // Destroy all target selection views
            
            Debug.Log("Unset playing card");
        }

        public override void AddCardsToSelection(List<DanmakuCardBaseView> cardViews)
        {
            CardSelectionView.ShowSelection();
            CardSelectionView.AddCardsToSelection(cardViews);
        }
        
    }
}