using System;
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRSessionMenuUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _keywordText;
        
        
        [SerializeField] private Image _backgroundImage;
        
        [SerializeField] private Button _endStepButton;
        
        [SerializeField] private List<OutcomeToBackgroundColor> _outcomeToBackgroundColors;
        
        
        [Serializable]
        class OutcomeToBackgroundColor
        {
            public MenuOutcomeEnum Outcome;
            public Sprite Sprite;
        }
        
        public void SetMenu(DanmakuSessionMenu sessionMenu)
        {
            
            SetMenuDescription(sessionMenu.Detail.Value);
            sessionMenu.Detail.OnValueChanged += SetMenuDescription;
            
            if (sessionMenu.ChoiceAction != ChoiceActionEnum.Confirm)
            {
                return;
            }
            
            SetOneTimeButtonAction(sessionMenu.TryEndSession);
        }

        private void SetMenuDescription(DanmakuSessionMenuDetail oldValue, DanmakuSessionMenuDetail newValue)
        {
            SetMenuDescription(newValue);
        }

        private void SetMenuDescription(DanmakuSessionMenuDetail menuDetail)
        {
            _titleText.text = menuDetail.Title;
            _keywordText.text = menuDetail.Keyword;
            
            var sprite = _outcomeToBackgroundColors.Find(x => x.Outcome.ToString() == menuDetail.MenuOutcome.ToString()).Sprite;
            _backgroundImage.sprite = sprite;
        }
        
        
        private void SetOneTimeButtonAction(Func<bool> tryEndFunc)
        {
            _endStepButton.onClick.AddListener(() =>
            {
                if (tryEndFunc != null && tryEndFunc.Invoke())
                {
                    _endStepButton.onClick.RemoveAllListeners();
                }
                
            });
        }
        
    }
}