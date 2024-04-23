using System;
using BNG;
using UnityEngine;


namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRPlayerUI : MonoBehaviour
    {
        [SerializeField] private Button _endStepButton;
        
        public void SetOneTimeButtonAction(Action action)
        {
            _endStepButton.onButtonDown.AddListener(() =>
            {
                _endStepButton.onButtonDown.RemoveAllListeners();
                action?.Invoke();
            });
        }
        
    }
}