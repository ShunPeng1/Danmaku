using System;
using UnityEngine;
using UnityEngine.UI;


namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRSessionMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _endStepButton;
        
        public void SetOneTimeButtonAction(Func<bool> tryEndFunc, Action endAction = null)
        {
            _endStepButton.onClick.AddListener(() =>
            {
                if (tryEndFunc != null && tryEndFunc.Invoke())
                {
                    _endStepButton.onClick.RemoveAllListeners();
                    endAction?.Invoke();
                }
                
            });
        }
        
    }
}