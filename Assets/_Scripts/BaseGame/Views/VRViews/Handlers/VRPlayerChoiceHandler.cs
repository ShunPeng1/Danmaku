using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BaseGame.Views.Basics;
using _Scripts.BaseGame.Views.Basics.UI.PlayerSelection;
using _Scripts.BaseGame.Views.Positions;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.VRViews.Handlers
{
    public class VRPlayerChoiceHandler : DanmakuSessionChoiceBaseHandler
    {
        [SerializeField] private PlayerStandingPositionMap _playerStandingPositionMap;
        
        [Header("Prefabs")]
        [SerializeField] private PlayerTargetSelectionButton _playerTargetSelectionButtonPrefab;
        private List<PlayerTargetSelectionButton> _playerSelectionButtons = new();
        
        protected override void Awake()
        {
            base.Awake();
            
            OnSessionChoiceSet += SetPlayerChoice;
            OnSessionChoiceUnset += ClearPlayerTargetSelection;
            
        }

        private void SetPlayerChoice(DanmakuSessionChoice choice)
        {
            var activatorPlayer = choice.Menu.Activator as DanmakuPlayerModel;
            var targetables = choice.Targetables.OfType<DanmakuPlayerModel>().ToList();
            SetPlayerTargetSelection(activatorPlayer, targetables);
            
        }

        public void SetPlayerTargetSelection(DanmakuPlayerModel activatorPlayer, List<DanmakuPlayerModel> targetables)
        {
            var playerGroupModel = InteractionSystem.InteractionController.PlayerGroupModel;
            int totalPlayers = playerGroupModel.Players.Count;
            int playerIndex = playerGroupModel.Players.IndexOf(activatorPlayer);
            
            foreach(var playerModel in playerGroupModel.Players)
            {
                var isTargetable = targetables.Contains(playerModel);
                var buttonTransform = _playerStandingPositionMap.GetPlayerPosition(totalPlayers, playerIndex, playerIndex);
                
                var playerSelectionButton = Instantiate(_playerTargetSelectionButtonPrefab, transform);
                
                playerSelectionButton.transform.position = buttonTransform.position;
                playerSelectionButton.transform.rotation = buttonTransform.rotation;
                
                    
                playerSelectionButton.SetPlayerTargetSelection(this);
                playerSelectionButton.SetPlayerModel(playerModel, isTargetable);
                _playerSelectionButtons.Add(playerSelectionButton);
            }
        }
        
        public void ClearPlayerTargetSelection(DanmakuSessionChoice danmakuSessionChoice)
        {
            foreach (var playerSelectionButton in _playerSelectionButtons)
            {
                Destroy(playerSelectionButton.gameObject);
            }
            _playerSelectionButtons.Clear();
        }

        public void SelectPlayer(DanmakuPlayerModel playerModel)
        {
            SessionChoice.SelectTarget(playerModel);
        }
        
    }
}