using System;
using _Scripts.BaseGame.Views.VRViews.Handlers;
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
        [SerializeField] private GameObject _selectedIndicator;

        private VRPlayerChoiceHandler _playerChoiceHandler;
        private DanmakuPlayerModel _playerModel;
        
        
        public void SetPlayerTargetSelection(VRPlayerChoiceHandler playerChoiceHandler)
        {
            _playerChoiceHandler = playerChoiceHandler;
        }

        public void SetPlayerModel(DanmakuPlayerModel playerModel, bool isTargetable)
        {
            _playerModel = playerModel;

            var characterCardData = playerModel.CharacterCardHandModel.GetFrontCard<DanmakuCharacterCardModel>().CharacterCardData;
            _characterImage.sprite = characterCardData.CardIllustration;
            _playerName.text = characterCardData.CardName;
            
            _button.interactable = isTargetable;
            if (isTargetable)
            {
                _button.onClick.AddListener(SelectPlayer);
            }
        }
        
        public void SetIndicator(bool isActive)
        {
            _selectedIndicator.SetActive(isActive);
        }

        private void SelectPlayer()
        {
            _playerChoiceHandler.SelectPlayer(this,_playerModel);
        }
        
        
        
    }
}