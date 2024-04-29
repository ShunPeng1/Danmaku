using System;
using BNG;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRMainDeckCardView : DanmakuMainDeckCardBaseView
    {
        [SerializeField] private TMP_Text _cardNameText;

        public bool IsPlayable { get; private set; }
        
        private Grabbable _grabbable;
        private Rigidbody _rigidbody;
        
        public bool IsMoveByTween { get; private set; }
        
        private Tween _moveTween;
        private Tween _rotateTween;

        private void Awake()
        {
            _grabbable = GetComponent<Grabbable>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _cardNameText.text = CardModel.DeckCardData.CardName;
        }

        public void TweenMove(Vector3 moveTo, Vector3 rotateTo, float duration, Ease ease = Ease.InOutCubic, Action onComplete = null)
        {
            _rigidbody.isKinematic = true;
            IsMoveByTween = true;
            _grabbable.enabled = false;
            _moveTween = transform.DOMove(moveTo, duration).SetEase(ease);
            _rotateTween = transform.DORotate(rotateTo, duration).SetEase(ease).OnComplete(() =>
            {
                _rigidbody.isKinematic = false;
                IsMoveByTween = false;
                _grabbable.enabled = true;
                onComplete?.Invoke();
            });
        }

        public override void CheckPlayable()
        {
            IsPlayable = CardModel.IsPlayable();
        }

        public override void SetNotPlayable()
        {
            IsPlayable = false;
        }
    }
}