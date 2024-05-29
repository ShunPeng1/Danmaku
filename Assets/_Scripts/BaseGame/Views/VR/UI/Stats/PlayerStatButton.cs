using _Scripts.BaseGame.Views.VRViews.Handlers;
using _Scripts.CoreGame.InteractionSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BaseGame.Views.VR.UI.Stats
{
    public class PlayerStatButton : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _selectedIndicator;
        
        private DanmakuPlayerModel _playerModel;
        private VRStatUI _statUI;
        
        public void SetStatUI(VRStatUI statUI)
        {
            _statUI = statUI;
        }
        
        
        public void SetPlayerModel(DanmakuPlayerModel playerModel)
        {
            _playerModel = playerModel;

            var characterCardData = playerModel.CharacterCardHandModel.GetFrontCard<DanmakuCharacterCardModel>().CharacterCardData;
            _characterImage.sprite = characterCardData.CardIllustration;
            _playerName.text = characterCardData.CardName;
            
            _button.onClick.AddListener(SelectPlayer);
        }

        private void SelectPlayer()
        {
            _statUI.SelectPlayer(this,_playerModel);
        }

        public void SetIndicator(bool isActive)
        {
            _selectedIndicator.SetActive(isActive);
        }

    }
}