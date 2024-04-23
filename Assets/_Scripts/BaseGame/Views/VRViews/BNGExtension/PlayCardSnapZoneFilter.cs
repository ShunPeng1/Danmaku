using System;
using BNG;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.BNGExtension
{
    public class PlayCardSnapZoneFilter : SnapZoneFilter
    {
        
        [SerializeField] private bool _isActivated = true;

        private void Awake()
        {
            AddMustIncludeFilter(IncludeIsPlayableCard);
            AddMustExcludeFilter(ExcludeIsTweeningCard);
        }

        private bool ExcludeIsTweeningCard(Grabbable arg)
        {
            var cardView = arg.GetComponent<VRMainDeckCardView>();
            if (cardView == null)
            {
                return false;
            }
            
            return cardView.IsMoveByTween;
            
        }

        private bool IncludeIsPlayableCard(Grabbable grabbable)
        {
            var cardView = grabbable.GetComponent<VRMainDeckCardView>();
            
            if (cardView == null)
            {
                return false;
            }
            
            bool isPlayable = cardView.CardModel.IsPlayable();
            
            return isPlayable;
        }

        public void SetActivated(bool isActivated)
        {
            _isActivated = isActivated;
        }
       
        public override bool CheckSnappable(Grabbable grabbable)
        {
            if (!_isActivated)
            {
                return true;
            }
            
            foreach (var filter in MustIncludeFilters)
            {
                if (!filter(grabbable))
                {
                    return false;
                }
            }
            
            foreach (var filter in MustExcludeFilters)
            {
                if (filter(grabbable))
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}