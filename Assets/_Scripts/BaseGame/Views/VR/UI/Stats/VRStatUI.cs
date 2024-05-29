using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views.Positions;
using _Scripts.CoreGame.InteractionSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BaseGame.Views.VR.UI.Stats
{
    public class VRStatUI : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private GameObject _statPanel;
        [SerializeField] private TMP_Text _lifeText;
        [SerializeField] private TMP_Text _handSizeText;
        [SerializeField] private TMP_Text _distanceText;
        [SerializeField] private TMP_Text _rangeText;
        [SerializeField] private TMP_Text _danmakuCardPlayedCountText;
        [SerializeField] private TMP_Text _spellCardPlayedCountText;
        
        [SerializeField] private string _format = "{0} / {1}";

        [Header("Prefabs")]
        [SerializeField] private PlayerStandingPositionMap _playerStandingPositionMap;
        [SerializeField] private PlayerStatButton _playerStatButtonPrefab;

        private DanmakuInteractionSystem _interactionSystem;
        private DanmakuPlayerBaseView _playerBaseView;
        
        private readonly List<PlayerStatButton> _playerStatButtons = new();
        private PlayerStatButton _selectingButton;
        private DanmakuPlayerModel _playerModel;

        private void Awake()
        {
            _statPanel.SetActive(false);
            
            _interactionSystem = GetComponentInParent<DanmakuInteractionSystem>();
            _playerBaseView = GetComponentInParent<DanmakuPlayerBaseView>();
            
            _interactionSystem.OnFinishDraw += InitializePlayerStatButton;
        }

        public void InitializePlayerStatButton()
        {
            var playerGroupModel = _interactionSystem.InteractionController.PlayerGroupModel;
            int totalPlayers = playerGroupModel.Players.Count;
            int playerIndex = playerGroupModel.Players.IndexOf(_playerBaseView.PlayerModel);
            
            foreach(var playerModel in playerGroupModel.Players)
            {
                var buttonTransform = _playerStandingPositionMap.GetPlayerPosition(totalPlayers, playerModel.PlayerId, playerIndex);
                
                var playerSelectionButton = Instantiate(_playerStatButtonPrefab, transform);
                
                playerSelectionButton.transform.position = buttonTransform.position;
                playerSelectionButton.transform.rotation = buttonTransform.rotation;
                
                    
                playerSelectionButton.SetStatUI(this);
                playerSelectionButton.SetPlayerModel(playerModel);
                _playerStatButtons.Add(playerSelectionButton);
            }
        }
        
        public void ClearPlayerTargetSelection(DanmakuSessionChoice danmakuSessionChoice)
        {
            foreach (var playerSelectionButton in _playerStatButtons)
            {
                Destroy(playerSelectionButton.gameObject);
            }
            
            _playerStatButtons.Clear();
        }
        
        
        public void SelectPlayer(PlayerStatButton selectingButton, DanmakuPlayerModel playerModel)
        {
            
            foreach (var selectionButton in _playerStatButtons)
            {
                if (selectionButton == selectingButton)
                {
                    selectionButton.SetIndicator(true);
                    continue;
                }
                
                selectionButton.SetIndicator(false);
            }
            
            if (_selectingButton != null && _playerModel != null && _selectingButton != selectingButton)
            {
                UnsubscribeToChangeStat(_playerModel);
                _selectingButton.SetIndicator(false);
            }

            _selectingButton = selectingButton;
            _playerModel = playerModel;

            SubscribeToChangeStat(_playerModel);
            ShowStat(playerModel);
        }

        private void SubscribeToChangeStat(DanmakuPlayerModel playerModel)
        {
            //TODO: Implement this
        }
        
        private void UnsubscribeToChangeStat(DanmakuPlayerModel playerModel)
        {
            //TODO: Implement this
        }

        private void ShowStat(DanmakuPlayerModel playerModel)
        {
            _statPanel.SetActive(true);
            
            _lifeText.text = string.Format(_format, playerModel.Life.Get(), playerModel.Life.GetMaxValue());
            _handSizeText.text = string.Format(_format, playerModel.DeckCardHandModel.Cards.Count.ToString(), playerModel.HandSize.Get());
            _distanceText.text = playerModel.Distance.Get().ToString();
            _rangeText.text = playerModel.Range.Get().ToString();
            _danmakuCardPlayedCountText.text = string.Format(_format, playerModel.DanmakuCardPlayedCount.Get(), playerModel.DanmakuCardPlayedCount.GetMaxValue());
            _spellCardPlayedCountText.text = string.Format(_format, playerModel.SpellCardPlayedCount.Get(), playerModel.SpellCardPlayedCount.GetMaxValue());
            
            
        }
        
        private void HideStat()
        {
            _statPanel.SetActive(false);
        }
    }
}