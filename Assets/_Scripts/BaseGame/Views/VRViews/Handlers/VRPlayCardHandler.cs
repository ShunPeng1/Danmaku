using System;
using System.Collections.Generic;
using BNG;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRPlayCardHandler : MonoBehaviour
    {
        [SerializeField] private TargetSelectionCanvas _targetSelectionCanvas;
        [SerializeField] private SnapZone _playCardSnapZone;
        private VRMainDeckCardView _playingCard;

        private void Awake()
        {
            _playCardSnapZone.OnSnapEvent.AddListener(SetPlayingCard);
            _playCardSnapZone.OnDetachEvent.AddListener(UnsetPlayingCard);
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

        
    }
}