using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCardSelectionView : DanmakuCardSelectionBaseView
    {
        
        [SerializeField] private Transform _cardSelectionParent;
        [SerializeField] private Vector3 _cardSelectionOffset;
        
        [Header("Tween")]
        [SerializeField] private float _moveDuration = 0.5f;
        [SerializeField] private Ease _moveEase = Ease.InOutSine;
        
        
        public override void ShowCharacterCardsSelection(List<DanmakuCharacterCardBaseView> characterCardViews)
        {
            for (var index = 0; index < characterCardViews.Count; index++)
            {
                var characterCardView = characterCardViews[index];
                characterCardView.transform.SetParent(_cardSelectionParent);
                characterCardView.transform.DOLocalMove(Vector3.zero + _cardSelectionOffset * index, _moveDuration).SetEase(_moveEase);
                characterCardView.transform.DOLocalRotate(Vector3.zero, _moveDuration).SetEase(_moveEase);
            }
        }
    }
}