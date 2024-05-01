using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.BaseGame.Views.Positions;
using BNG;
using DG.Tweening;

using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCardSelectionView : DanmakuCardSelectionBaseView
    {
        [SerializeField] private GameObject _selectionView;
        [SerializeField] private SnapZoneCoordinator _snapZoneCoordinator;

        [Header("Tween Settings")]
        [SerializeField] private float _addCardMoveDuration = 0.5f;
        [SerializeField] private Ease _tweenEase = Ease.InOutCubic;
        
        
        public override void ShowSelection()
        {
            _selectionView.SetActive(true);
        }

        public override void HideSelection()
        {
            _selectionView.SetActive(false);
        }

        public override void AddCardsToSelection(List<DanmakuCardBaseView> characterCardViews)
        {
            List<Grabbable> grabbables = characterCardViews.Select(characterCardView => characterCardView.GetComponent<Grabbable>()).ToList();

            var snapZones = _snapZoneCoordinator.CreateSnapZones(characterCardViews.Count);

            
            for (var index = 0; index < characterCardViews.Count; index++)
            {
                var grabbable = grabbables[index];
                var snapZone = snapZones[index];
                var vrCardView = (VRCharacterCardView)characterCardViews[index];
                
                vrCardView.TweenMove(snapZone.transform.position,
                    snapZone.transform.rotation.eulerAngles,
                    _addCardMoveDuration,
                    _tweenEase,
                    () => {snapZone.GrabGrabbable(grabbable); });

            }
        }
    }
}