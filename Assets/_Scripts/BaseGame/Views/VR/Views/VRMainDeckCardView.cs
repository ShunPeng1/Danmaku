using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems;
using BNG;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRMainDeckCardView : DanmakuMainDeckCardBaseView
    {
        [SerializeField] private TMP_Text _cardNameText;

        public bool IsMoveByTween { get; private set; }
        public bool IsPlayable { get; private set; }
        
        private DanmakuInteractionViewRepo _viewRepo;
        private DeckCardScriptableData _deckCardData;
        
        private Grabbable _grabbable;
        private Rigidbody _rigidbody;
        
        private Tween _moveTween;
        private Tween _rotateTween;

        private void Awake()
        {
            _grabbable = GetComponent<Grabbable>();
            _rigidbody = GetComponent<Rigidbody>();
            _viewRepo = GetComponentInParent<DanmakuInteractionViewRepo>();
        }

        private void Start()
        {
            DanmakuMainDeckCardModel mainDeckCardModel = (DanmakuMainDeckCardModel)CardModel;
            _cardNameText.text = mainDeckCardModel.DeckCardData.CardName;
        }

        public override void SetCardModel(IDanmakuCard cardModel)
        {
            if (cardModel is DanmakuMainDeckCardModel mainDeckCardModel)
            {
                CardModel = mainDeckCardModel;
                _deckCardData = mainDeckCardModel.DeckCardData;

                mainDeckCardModel.OnCardExecuted += VisualizeCardExecution;
            }
            else
            {
                Debug.LogError("Wrong card model type");
            }
        }

        private void VisualizeCardExecution(IDanmakuCardRule rule, IDanmakuActivator activator, List<IDanmakuTargetable> targetables)
        {
            var ruleType = rule.GetType().ToString();
            ruleType = ruleType.Substring(ruleType.LastIndexOf('.') + 1);
            
            var ruleData = _deckCardData.CardRulesScriptableData.FirstOrDefault(ruleData => ruleData.CardRuleName == ruleType);

            if (ruleData != null && ruleData.VisualizerPrefab != null)
            {
                
                GameObject activatorGameObject = _viewRepo.GetActivatorView(activator);
                List<GameObject> targetableGameObjects = _viewRepo.GetTargetableViews(targetables);

                if (activatorGameObject != null && targetableGameObjects != null)
                {
                    var executionVisualizer = Instantiate(ruleData.VisualizerPrefab, activatorGameObject.transform.position, Quaternion.identity);

                    executionVisualizer.Visualize(activatorGameObject, targetableGameObjects);
                }
            }
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