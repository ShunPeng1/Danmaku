using System.Collections.Generic;
using _Scripts.BaseGame.Views.Positions;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI.PlayerSelection
{
    public class PlayerTargetSelection : MonoBehaviour, ITargetSelectionView
    {
        [SerializeField] private PlayerStandingPositionMap _playerStandingPositionMap;

        
        [Header("Prefabs")]
        [SerializeField] private PlayerSelectionButton _playerSelectionButtonPrefab;
        
        
        private List<PlayerSelectionButton> _playerSelectionButtons = new();
        
        public void Initialize()
        {
            
        }
        
        public void SetPlayerTargetSelection(int playerIndex)
        {
            
        }
    }
}