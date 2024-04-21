using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Button _endStepButton;
        
        public void SetOneTimeButtonAction(Action action)
        {
            _endStepButton.onClick.AddListener(() =>
            {
                _endStepButton.onClick.RemoveAllListeners();
                action?.Invoke();
            });
        }
        
    }
}