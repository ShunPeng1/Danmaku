using System;
using _Scripts.CoreGame.InteractionSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BaseGame.Views.Basics.UI.PlayerSelection
{
    public class PlayerTargetSelectionButton : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private Button _button;

        
        private void Awake()
        {
            _button.onClick.AddListener(SelectPlayer);
        }

        private void Initialize(DanmakuPlayerModel playerModel)
        {
            
            
        }

        private void SelectPlayer()
        {
            Debug.Log("Selecting player");
        }
    }
}