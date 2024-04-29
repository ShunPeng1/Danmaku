using _Scripts.BaseGame.Views.Basics.UI.PlayerSelection;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class TargetSelectionCanvas : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private PlayerTargetSelection _playerTargetSelectionPrefab;
        
        private ITargetSelectionView _targetSelectionView;
        
        
        public void ShowPlayerTargetSelection()
        {
            _targetSelectionView = Instantiate(_playerTargetSelectionPrefab, transform);
            _targetSelectionView.Initialize();
        }
    }
}