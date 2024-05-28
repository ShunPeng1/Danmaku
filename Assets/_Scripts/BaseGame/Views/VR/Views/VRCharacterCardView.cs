using System;
using _Scripts.CoreGame.InteractionSystems;
using BNG;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCharacterCardView : DanmakuCharacterCardBaseView
    {
        [SerializeField] private TMP_Text _cardNameText;
        [SerializeField] private GameObject _cardTemplate;

        private DanmakuCharacterCardModel _characterCardModel;
        
        
        private Grabbable _grabbable;
        private Rigidbody _rigidbody;
        private SpriteRenderer _spriteRenderer;
        
        public bool IsMoveByTween { get; private set; }
        
        private Tween _moveTween;
        private Tween _rotateTween;

        private void Awake()
        {
            _grabbable = GetComponent<Grabbable>();
            _rigidbody = GetComponent<Rigidbody>();
            _spriteRenderer = _cardTemplate.GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            DanmakuCharacterCardModel characterCardModel = (DanmakuCharacterCardModel)CardModel;
            _cardNameText.text = characterCardModel.CharacterCardData.CardName;
            _spriteRenderer.sprite = characterCardModel.CharacterCardData.CardIllustration;
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

    }
}